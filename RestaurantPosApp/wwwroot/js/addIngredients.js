function addIngredientHandler() {
    let itemNum = $(".form-row").length;
    console.log(itemNum);
    $("#add-ingredient").on('click', function () {
        var parentDiv = $("<div>", { "class": "form-row w-100 my-2" });
        parentDiv.html(
            `
            <div class="col-sm-4">
                <label class="control-label" for="z${itemNum}__Name">Ingredient Name</label>
                <input class="form-control" type="text" data-val="true" data-val-required="The Ingredient Name field is required." id="z${itemNum}__Name" name="[${itemNum}].Name" value="">
                <span class="text-danger field-validation-valid" data-valmsg-for="[${itemNum}].Name" data-valmsg-replace="true"></span>
            </div>
            <div class="col-sm-4">
                <label class="control-label" for="z${itemNum}__BaseUnitOfWeight">Base Unit of Weight</label>
                <input class="form-control" placeholder="Weight in grams" type="number" data-val="true" data-val-required="The Base Unit of Weight field is required." id="z${itemNum}__BaseUnitOfWeight" name="[${itemNum}].BaseUnitOfWeight" value="">
                <span class="text-danger field-validation-valid" data-valmsg-for="[${itemNum}].BaseUnitOfWeight" data-valmsg-replace="true"></span>
            </div>
            <div class="col-sm-4">
                <label class="control-label" for="z${itemNum}__PricePerUnit">Price Per Unit </label>
                <input class="form-control" type="text" data-val="true" data-val-number="The field Price Per Unit  must be a number." data-val-required="The Price Per Unit  field is required." id="z${itemNum}__PricePerUnit" name="[${itemNum}].PricePerUnit" value="">
                <span class="text-danger field-validation-valid" data-valmsg-for="[${itemNum}].PricePerUnit" data-valmsg-replace="true"></span>
            </div>
            `);

        parentDiv.insertBefore($(".form-group"));

        resetFormValidator("#ingredients-form");
        itemNum++;
        return false;
    });
}

function resetFormValidator(formId) {
    $(formId).removeData('validator');
    $(formId).removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(formId);
}


addIngredientHandler();