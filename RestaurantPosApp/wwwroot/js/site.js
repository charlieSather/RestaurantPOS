// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function addToOrder(obj) {
    var $obj = $(obj);

    var name = $obj.data("menuitem-name");
    var id = $obj.data("menuitem-id");
    var price = $obj.data("menuitem-price");

    var nameColumn = $("<span>").html(name);
    var priceColumn = $("<span>", { "name": "price", "data-val": price }).html(price);
    var removeButton = $("<button>", { "class": "btn btn-danger btn-sm", "type": "button" }).html("Remove");
    var quantity = $("<input>", { "class": "quantityInput", "type": "number", "value": 1, "min": 1 });

    removeButton.on("click", removeItem);
    quantity.on("change", updateQuantity);

    var div = $("<div>", { "class": "order-item", "name": "OrderMenuItem", "value": id }).append(nameColumn, priceColumn, quantity, removeButton);

    $("#orderBox").append(div);
    updateOrderTotal();

    return false;
}

function updateOrderTotal() {
    let total = 0;

    $("[name='OrderMenuItem']").each((index, el) => {
        let price = parseFloat($(el).children("span[name='price']").first().text());
        let quantity = $(el).children("input").first().val();
        total += price * quantity;
    });
    $("#orderTotal").text(total.toFixed(2));
}

function updateQuantity(event) {
    let input = event.target;

    if (isNaN(input.value) || input.value <= 0) {
        input.value = 1;
    }
    updateOrderTotal();
}

function removeItem(event) {
    event.target.parentElement.remove();
    updateOrderTotal();
}

function submitOrder() {
    let total = $("#orderTotal").text();
    var orderitems = [];

    $("[name='OrderMenuItem']").each((index, el) => {
        let quantity = $(el).children("input").first().val();
        let menuItemId = $(el).attr("value");

        orderitems.push({
            quantity: quantity,
            menuItemId: menuItemId,
        });
    });

    var placedOrder = {
        Total: total,
        OrderedItems: orderitems
    };


    $.ajax({
        type: "POST",
        url: "/Restaurant/CreateOrder",
        data: {
            placedOrder: placedOrder
        },
        success: (data) => {
            if (data.errorMessage != undefined) {
                alert(data.errorMessage);
            }
            else {
                alert("Order has been placed!");
                clearOrderWindow();
            }
        },
        error: (data) => {
            alert("There was an error processing the request.\nCheck console for details.");
            console.log(data);
        }
    });
}

function clearOrderWindow() {
    $("#orderBox").empty();
    updateOrderTotal();
}

function addAnotherIngredient(obj) {
    var $obj = $(obj);

    var parentDiv = $("<div>", { "class": "form-inline border my-3" });
    var quantityDiv = $("<div>", { "class": "form-group p-sm-4" });
    var quantityLabel = $("<label>", { "class": "control-label", "for": "MenuItemIngredient_Quantity" }).html("Amount in Grams");
    var quantityInput = $("<input>", {
        "class": "form-control",
        "type": "number",
        "data-val": "true",
        "data-val-required": "THe Amount in Grams field is required.",
        "id": "MenuItemIngredient_Quantity",
        "name": "MenuItemIngredient_Quantity",
        "value": ""
    });

    var quantitySpan = $("<span>", {
        "class": "text-danger field-validation-valid",
        "data-valmsg-for": "MenuItemIngredient.Quantity",
        "data-valmsg-replace": "true"
    });

    quantityDiv.append(quantityLabel, quantityInput, quantitySpan);
    parentDiv.append(quantityDiv);

    console.log(parentDiv);
    parentDiv.appendTo($("#recipeContainer"));

    console.log($obj);

    return false;

}

function setupIngredientHandler() {

    let itemNum = $("#recipeContainer > div ").length;
    console.log(itemNum);
    $("#addAnotherButton").on('click', function () {
        var parentDiv = $("<div>", { "class": "border my-3" });
        var quantityDiv = $("<div>", { "class": "form-group p-sm-4" });
        var quantityLabel = $("<label>", { "class": "control-label", "for": `MenuItemIngredient_Quantity` }).html("Amount in Grams");
        var quantityInput = $("<input>", {
            "class": "form-control",
            "type": "number",
            "data-val": "true",
            "data-val-required": "THe Amount in Grams field is required.",
            "id": `Recipe_${itemNum}__Quantity`,
            "name": `Recipe[${itemNum}].Quantity`,
            "value": ""
        });

        var quantitySpan = $("<span>", {
            "class": "text-danger field-validation-valid",
            "data-valmsg-for": `Recipe[${itemNum}].Quantity`,
            "data-valmsg-replace": "true"
        });

        quantityDiv.append(quantityLabel, quantityInput, quantitySpan);
        parentDiv.append(quantityDiv);
        var selectList = $("#selectList").clone();

        selectList.children("select").attr("id", `Recipe_${itemNum}__IngredientId`);
        selectList.children("select").attr("name", `Recipe[${itemNum}].IngredientId`);
        selectList.children("span").attr("data-valmsg-for", `Recipe[${itemNum}].IngredientId`);


        parentDiv.append(selectList);

        $("#recipeContainer").append(parentDiv);

        itemNum++;
        return false;

    });
}

setupIngredientHandler();