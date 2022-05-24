$("#CategoryId").change(function () {
    var currentId = $(this).val();
    $.ajax({
        url: "/admin/products/Index/LoadChildCategories?parentId=" + currentId,
        type: "get"
    }).done(function (data) {
        $("#SubCategoryId").html(data);
    });
});
$("#SubCategoryId").change(function () {
    var currentId = $(this).val();
    $.ajax({
        url: "/admin/products/Index/LoadChildCategories?parentId=" + currentId,
        type: "get"
    }).done(function (data) {
        $("#SecondarySubCategoryId").html(data);
    });
});
function AddRow() {
    var count = $("#rowCount").val();

    for (var i = 0; i < count; i++) {
        $("#table-body").append(
            "<tr>" +
            "<td><input type='text' autocomplete='off' name='Keys' class='form-control'/></td>" +
            "<td><input type='text' autocomplete='off' name='Values' class='form-control'/></td></tr>"
        );
    }
}