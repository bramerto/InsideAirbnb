var geoJson;
var lastId;

var map = new mapboxgl.Map({
    container: "map",
    style: "mapbox://styles/mapbox/light-v10",
    center: [4.896645, 52.367912],
    zoom: 11.2
});

map.on("load", async function () {
    await $.ajax({
            url: "/api/listings",
            method: "GET",
            dataType: "json"
        })
        .done((value) => {
            geoJson = value;
        });

    map.addSource("listings", {
        type: "geojson",
        data: geoJson,
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
            'circle-radius': 3,
            'circle-stroke-width': 1,
            'circle-stroke-color': "#fff"
        }
    });

    // inspect a cluster on click
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

    map.on("click", "unclustered-point", async function (e) {
        var coordinates = e.features[0].geometry.coordinates.slice();
        var id = e.features[0].properties.id;

        if (id !== lastId) {
            lastId = id;

            $.ajax({
                url: "/api/listings/" + id,
                method: "GET",
                dataType: "json"
            }).done((value) => {

                $('#locationId').val(value.Id);
                $('#locationUrl').val(value.ListingUrl);
                $('#locationName').val(value.Name);
                $('#locationSummary').val(value.Summary);
                $('#locationDescription').val(value.Description);
                $('#locationNeighbourhood').val(value.Neighbourhood);
                $('#locationZipcode').val(value.Zipcode);
                $('#locationSquareFeet').val(value.SquareFeet);
                $('#locationPrice').val(value.Price);
                $('#locationWeeklyPrice').val(value.WeeklyPrice);
                $('#locationMonthlyPrice').val(value.MonthlyPrice);
                $('#locationSecurityDeposit').val(value.SecurityDeposit);
                $('#locationCleaningFee').val(value.CleaningFee);
                $('#locationMinimumNights').val(value.MinimumNights);
                $('#locationMaximumNights').val(value.MaximumNights);
            });
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
});