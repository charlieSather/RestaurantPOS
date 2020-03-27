function generateShoppingList() {
    $.ajax({
        type: "Get",
        url: "/Owner/GenerateShoppingList",
        success: (data) => {
            if (data.message != undefined) {
                alert(data.message);
            }
            else {
            }
            console.log(data);
        },
        error: (data) => {
            alert("There was an error processing the request.\nCheck console for details.");
            console.log(data);
        }
    });
}

function filterByDate() {
    var start = $("#start-input").val();
    var end = $("#end-input").val();
    var data = {
        start: start,
        end: end
    };


    $.ajax({
        type: "Get",
        url: "/Owner/GetTopSellingMenuItemsByDateRange",
        data: data,
        error: (data) => {
            alert("There was an error processing the request.\nCheck console for details.");
            console.log(data);
        }
    })
    .then((data) => {
        $("#chart-container").hide();
        $("#chart-container").html(data);
        $("#chart-container").fadeIn(500);
    });
    }


$(document).ready(() => {
    $("#generate-list-button").click(generateShoppingList);
    $("#filter-button").click(filterByDate)
});