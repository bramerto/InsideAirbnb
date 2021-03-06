﻿if (!isEmpty($("#map"))) {
    var geoJson;
    var lastId;

    var map = new mapboxgl.Map({
        container: "map",
        style: "mapbox://styles/mapbox/light-v10",
        center: [4.896645, 52.367912],
        zoom: 11.2
    });

    var currencyFormatter = new Intl.NumberFormat("nl-NL", { style: "currency", currency: "USD" });
    var numberFormatter = new Intl.NumberFormat("nl-NL");

    map.on("load", function () {

        /* MAP INITIALISE */
        $.ajax({
            url: "/api/listings",
            method: "GET",
            dataType: "json"
        })
            .done((data) => setMapData(data))
            .fail((error) => alert("Er is iets misgegaan met het ophalen van de initiële mapdata."));

        map.addSource("listings", {
            type: "geojson",
            data: null,
            cluster: true,
            clusterMaxZoom: 13, // Max zoom to cluster points on
            clusterRadius: 100 // Radius of each cluster when clustering points (defaults to 50)
        });

        map.addLayer({
            id: "clusters",
            type: "circle",
            source: "listings",
            filter: ["has", "point_count"],
            paint: {
                'circle-color': [
                    "step",
                    ["get", "point_count"],
                    "#51bbd6", // Circle is blue when point count is less than 100
                    100,
                    "#f1f075", // Circle is yellow when point count is between 100 and 750
                    750,
                    "#ff6961" // Circle is soft red when point count is greater than or equal to 750
                ],
                'circle-radius': [
                    "step",
                    ["get", "point_count"],
                    20, // Circle is 20px of radius when point count is less than 100
                    100,
                    30, // Circle is 30px of radius when point count is between 100 and 750
                    750,
                    40 // Circle is 40px of radius when point count is greater than or equal to 750
                ]
            }
        });

        map.addLayer({
            id: "cluster-count",
            type: "symbol",
            source: "listings",
            filter: ["has", "point_count"],
            layout: {
                'text-field': "{point_count_abbreviated}",
                'text-font': ["DIN Offc Pro Medium", "Arial Unicode MS Bold"],
                'text-size': 12
            }
        });

        map.addLayer({
            id: "unclustered-point",
            type: "circle",
            source: "listings",
            filter: ["!", ["has", "point_count"]],
            paint: {
                'circle-color': "#11b4da",
                'circle-radius': 4,
                'circle-stroke-width': 1,
                'circle-stroke-color': "#fff"
            }
        });

        /* EVENTS */
        map.on("click", "clusters", function (e) {
            var features = map.queryRenderedFeatures(e.point, {
                layers: ["clusters"]
            });
            var clusterId = features[0].properties.cluster_id;
            map.getSource("listings").getClusterExpansionZoom(
                clusterId,
                function (err, zoom) {
                    if (err) return;

                    map.easeTo({
                        center: features[0].geometry.coordinates,
                        zoom: zoom
                    });
                }
            );
        });

        map.on("click", "unclustered-point", function (e) {
            var coordinates = e.features[0].geometry.coordinates.slice();
            var id = e.features[0].properties.id;

            if (id !== lastId) {
                lastId = id;

                $.ajax({
                    url: "/api/listings/" + id,
                    method: "GET",
                    dataType: "json"
                })
                    .done((value) => {
                        $("#locationId").val(value.Id);
                        $("#locationUrl").val(value.ListingUrl);
                        $("#locationName").val(value.Name);
                        $("#locationReviewScoresRating").val(value.ReviewScoresRating + "/100");
                        $("#locationDescription").val(value.Description);
                        $("#locationNeighbourhood").val(value.Neighbourhood);
                        $("#locationZipcode").val(value.Zipcode);
                        $("#locationSquareFeet").val(value.SquareFeet);
                        $("#locationPrice").val(formatUsd(value.Price));
                        $("#locationWeeklyPrice").val(formatUsd(value.WeeklyPrice));
                        $("#locationMonthlyPrice").val(formatUsd(value.MonthlyPrice));
                        $("#locationSecurityDeposit").val(formatUsd(value.SecurityDeposit));
                        $("#locationCleaningFee").val(formatUsd(value.CleaningFee));
                        $("#locationMinimumNights").val(value.MinimumNights);
                        $("#locationMaximumNights").val(value.MaximumNights);
                    })
                    .fail((error) => alert("Er is iets misgegaan met het ophalen van een listing."));
            }

            // Ensure that if the map is zoomed out such that
            // multiple copies of the feature are visible, the
            // popup appears over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }
        });

        map.on("mouseenter", "unclustered-point", function () {
            map.getCanvas().style.cursor = "pointer";
        });
        map.on("mouseleave", "unclustered-point", function () {
            map.getCanvas().style.cursor = "";
        });

        map.on("mouseenter", "clusters", function () {
            map.getCanvas().style.cursor = "pointer";
        });
        map.on("mouseleave", "clusters", function () {
            map.getCanvas().style.cursor = "";
        });

        $("#submitFilter").on("click", async (e) => {
            var minPrice = $("#minPriceFilter").val();
            var maxPrice = $("#maxPriceFilter").val();
            var neighbourhood = $("#neighbourhoodFilter option:selected").val();
            var minReviewRate = $("#minReviewRateFilter").val();

            //add validation

            $.ajax({
                url: "api/listings",
                method: "POST",
                dataType: "json",
                data: {
                    minPrice,
                    maxPrice,
                    neighbourhood,
                    minReviewRate
                }
            })
                .done((data) => setMapData(data))
                .fail((error) => handleErrors(error));
        });

        /* FUNCTIONS */
        function setMapData(data) {
            map.getSource("listings").setData(data.geoJson);

            $("#totalLocations").html(numberFormatter.format(data.totalLocations));
            $("#staysPerMonth").html(numberFormatter.format(data.staysPerMonth));
            $("#collectionPerMonth").html(formatUsd(data.collectionPerMonth));
        }

        function formatUsd(value) {
            return currencyFormatter.format(value).replace("US", "").replace(/\s/g, "");
        }
    });
}