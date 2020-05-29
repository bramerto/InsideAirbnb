$(document).ready(function () {

    $.ajax({
        url: "api/neighbourhoods",
        method: "GET",
        dataType: "json"
    }).done((data) => {
        var options = "";
        for (var x = 0; x < data.length; x++) {
            options += '<option value="' + data[x].Id + '">' + data[x].Neighbourhood + "</option>";
        }
        $("#neighbourhoodFilter").html(options);
    });

    $("#submitFilter").on("click", (e) => {
        e.preventDefault();

        var minPrice = $("#minPriceFilter").val();
        var maxPrice = $("#maxPriceFilter").val();
        var neighbourhoodId = $("#neighbourhoodFilter").val();
        var minReviews = $("#minReviewsFilter").val();
        var maxReviews = $("#maxReviewsFilter").val();

        $.ajax({
            url: "api/listings",
            method: "POST",
            dataType: "json",
            data: {
                minPrice,
                maxPrice,
                neighbourhoodId,
                minReviews,
                maxReviews
            }
        }).done((data) => {
            alert(JSON.stringify(data));
        });
    });
});