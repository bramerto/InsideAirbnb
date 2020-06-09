$(document).ready(function () {

    $("#minReviewRateFilter").on("input", () => {
        $("#rangeIndicator").html($("#minReviewRateFilter").val());
    });
});

function handleErrors(error) {
    var message = "";
    for (const errorMessage of error.responseJSON.errors.values()) {
        console.log(errorMessage);
        message += errorMessage + "\n";
    }
    alert(message);
}