$(document).ready(function () {

    $.ajax({
        url: "api/neighbourhoods",
        method: "GET",
        dataType: "json"
    }).done((data) => {
        var options = '<option value="0">Selecteer...</option>';
        for (var x = 0; x < data.length; x++) {
            options += '<option value="' + data[x].Id + '">' + data[x].Neighbourhood + "</option>";
        }
        $("#neighbourhoodFilter").html(options);

    }).fail((error) => {
        alert("Er is iets misgegaan met het ophalen van buurten.");
    });

    $("#minReviewRateFilter").on("input", () => {
        $("#rangeIndicator").html($("#minReviewRateFilter").val());
    });
});