$(function () {
    var submitAutocompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        var $form = $input.parents("form:first");
        $form.submit();
    }
    var createAutocomplete = function () {
        var $input = $(this)
        var options = {
            source: $input.attr("data-product-autocomplete"),
            select:  submitAutocompleteForm
        }
        $input.autocomplete(options)
    }
    $("input[data-product-autocomplete]").each(createAutocomplete);
    showAdvancedSearchForm();
})
var showAdvancedSearchForm = function () {
    var $checkBox = $("#advanced");
    if ($checkBox.is(":checked")) {
        $("#searchForm").show()
    }
    else {
        $("#searchForm").hide()
    }
}
