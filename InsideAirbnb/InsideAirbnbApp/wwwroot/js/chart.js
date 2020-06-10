var availability = $("#availabilityChart");
var prices = $("#pricesChart");
var propertyType = $("#propertyTypeChart");

if (!isEmpty(availability) && !isEmpty(prices) && !isEmpty(propertyType)) {
    var availabilityCtx = document.getElementById("availabilityChart").getContext("2d");
    var pricesCtx = document.getElementById("pricesChart").getContext("2d");
    var propertyTypeCtx = document.getElementById("propertyTypeChart").getContext("2d");

    var availabilityChart;
    var pricesChart;
    var propertyTypeChart;

    $.ajax({
        url: "/api/listings/chart/average/availability",
        method: "GET",
        dataType: "json"
    })
        .done((data) => {
            availabilityChart = new Chart(availabilityCtx, {
                options: {
                    scales: {
                        yAxes: [
                            {
                                scaleLabel: {
                                    display: true,
                                    labelString: "Gemiddelde beschikbaarheid in dagen"
                                }
                            }],
                        xAxes: [{
                            gridLines: {
                                display: false
                            }
                        }]
                    }
                },
                data: data,
                type: "bar"
            });
        })
        .fail((error) => alert("Er is iets misgegaan met het ophalen van de grafiekdata."));

    $.ajax({
        url: "/api/listings/chart/average/prices",
        method: "GET",
        dataType: "json"
    })
        .done((data) => {
            pricesChart = new Chart(pricesCtx, {
                options: {},
                data: data,
                type: "pie"
            });
        })
        .fail((error) => alert("Er is iets misgegaan met het ophalen van de grafiekdata."));

    $.ajax({
        url: "/api/listings/chart/property/type",
        method: "GET",
        dataType: "json"
    })
        .done((data) => {
            propertyTypeChart = new Chart(propertyTypeCtx, {
                options: {
                    scales: {
                        xAxes: [{
                            scaleLabel: {
                                display: true,
                                labelString: "Soorten eigendommen"
                            },
                            gridLines: {
                                display: false
                            }
                        }]
                    }
                },
                data: data,
                type: "horizontalBar"
            });
        })
        .fail((error) => alert("Er is iets misgegaan met het ophalen van de grafiekdata."));
}

$("#neighbourhoodFilterGraph").on("change", () => {
    var neighbourhoodId = $("#neighbourhoodFilterGraph option:selected").val();

    //add validation

    $.ajax({
        url: "/api/listings/chart/average/availability/" + neighbourhoodId,
            method: "GET",
            dataType: "json"
        })
        .done((data) => updateData(availabilityChart, data))
        .fail((error) => handleErrors(error));

    $.ajax({
            url: "/api/listings/chart/average/prices/" + neighbourhoodId,
            method: "GET",
            dataType: "json"
        })
        .done((data) => updateData(pricesChart, data))
        .fail((error) => handleErrors(error));

    $.ajax({
        url: "/api/listings/chart/property/type/" + neighbourhoodId,
            method: "GET",
            dataType: "json"
        })
        .done((data) => updateData(propertyTypeChart, data))
        .fail((error) => handleErrors(error));
});

function updateData(chart, data) {
    chart.data = data;
    chart.update();
}