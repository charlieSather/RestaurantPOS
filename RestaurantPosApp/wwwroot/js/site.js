// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function addToOrder(obj) {
    var $obj = $(obj);

    var name = $obj.data("menuitem-name");
    var id = $obj.data("menuitem-id");
    var price = $obj.data("menuitem-price");

    var div = document.createElement("div");
    div.setAttribute("name", "MenuItem");
    div.setAttribute("value", id);
    div.innerHTML = name + " " + price;

    $("#orderBox").append(div);

    return false;
}

//<div class="form-inline border my-3">
//    <div class="form-group p-sm-4">
//        <label asp-for="MenuItemIngredient.Quantity" class="control-label"></label>
//        <input asp-for="MenuItemIngredient.Quantity" class="form-control" />
//        <span asp-validation-for="MenuItemIngredient.Quantity" class="text-danger"></span>
//    </div>
//    <div class="form-group p-sm-4">
//        <label asp-for="MenuItemIngredient.Ingredient" class="control-label"></label>
//        <select asp-for="MenuItemIngredient.Ingredient" class="form-control" asp-items="@ViewBag.ingredients"></select>
//        <span asp-validation-for="MenuItemIngredient.Ingredient" class="text-danger"></span>
//    </div>
//</div>


//<form asp-action="Index">
//    <div id="formInsert">

//    </div>
//    <button type="submit">Create Customers</button>
//</form>
//    <button type="button" id="addForm">Add Another Customer</button>



//    <script>
//        let itemNum = 0;
//    $("#addForm").on('click', function () {

//            console.log("Clicked! Item Num: " + itemNum);
//        $("#formInsert").append('<input type="text" id="z'+itemNum+'_Name" name="['+itemNum+'].Name" value>');
//        itemNum++;
//    });
//</script>



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
        "value" : ""
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


//    <script>
//        let itemNum = 0;
//    $("#addForm").on('click', function () {

//            console.log("Clicked! Item Num: " + itemNum);
//        $("#formInsert").append('<input type="text" id="z'+itemNum+'_Name" name="['+itemNum+'].Name" value>');
//        itemNum++;
//    });
//</script>

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

        //parentDiv.appendTo($("#recipeContainer"));

        itemNum++;
        return false;

    });
}


setupIngredientHandler();