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

    $("#minReviewRateFilter").on("input", () => {
        $("#rangeIndicator").html($("#minReviewRateFilter").val());
    });
});