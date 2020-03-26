function generateShoppingList() {
    $.ajax({
        type: "Get",
        url: "/Owner/GenerateShoppingList",
        success: (data) => {
            if (data.message != undefined) {
                alert(data.message, "SuccessERRRR");
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


$(document).ready(() => {
    $("#generate-list-button").click(generateShoppingList);
});