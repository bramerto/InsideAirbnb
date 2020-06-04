$(document).ready(function () {

    $("#minReviewRateFilter").on("input", () => {
        $("#rangeIndicator").html($("#minReviewRateFilter").val());
    });
});