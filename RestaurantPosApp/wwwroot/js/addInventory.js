function addInventoryHandler() {
    let itemNum = $(".form-row").length;
    $("#add-inventory").on('click', function () {
        var parentDiv = $("<div>", { "class": "form-row w-100 my-2" });
        parentDiv.html(
            `
            <div class="col-sm-3">
                <label class="control-label" for="z${itemNum}__AmountInGrams">Amount in grams</label>
                <input class="form-control" type="number" data-val="true" data-val-range="The field AmountInGrams must be between 0 and 1000000." data-val-range-max="1000000" data-val-range-min="0" data-val-required="The Amount in Grams field is required." id="z${itemNum}__AmountInGrams" name="[${itemNum}].AmountInGrams" value="">
                <span class="text-danger field-validation-valid" data-valmsg-for="[${itemNum}].AmountInGrams" data-valmsg-replace="true"></span>
            </div>
            <div class="col-sm-3">
                <label class="control-label" for="z${itemNum}__LowerThreshold">Low inventory threshold</label>
                <input class="form-control" type="number" data-val="true" data-val-range="The field LowerThreshold must be between 0 and 10000." data-val-range-max="10000" data-val-range-min="0" data-val-required="The Low inventory threshold field is required." id="z${itemNum}__LowerThreshold" name="[${itemNum}].LowerThreshold" value="">
                <span class="text-danger field-validation-valid" data-valmsg-for="[${itemNum}].LowerThreshold" data-valmsg-replace="true"></span>
            </div>`);
        var selectDiv = $("#selectDiv").clone();

        selectDiv.children("label").attr("for", `z${itemNum}__Ingredient`);
        selectDiv.children("select").attr({ "id": `z${itemNum}__IngredientId`, "name": `[${itemNum}].IngredientId`, "aria-describedby": `z${itemNum}__IngredientId-error` });

        parentDiv.prepend(selectDiv);

        parentDiv.insertBefore($(".form-group"));

        resetFormValidator("#inventory-form");

        itemNum++;
        return false;
    });
}

function resetFormValidator(formId) {
    $(formId).removeData('validator');
    $(formId).removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(formId);
}

addInventoryHandler();