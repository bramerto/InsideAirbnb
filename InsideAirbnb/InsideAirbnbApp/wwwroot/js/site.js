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

function isEmpty(obj) {
    for (var prop in obj) {
        if (obj.hasOwnProperty(prop)) {
            return false;
        }
    }

    return JSON.stringify(obj) === JSON.stringify({});
}