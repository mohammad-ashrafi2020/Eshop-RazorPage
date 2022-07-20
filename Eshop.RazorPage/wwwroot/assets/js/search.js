function filterCategory(slug) {
    $("#serachForm").submit();
}

function changePageId(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;
    search_params.set('pageId', pageId);
    url.search = search_params.toString();
    var new_url = url.toString();
    window.location.replace(new_url);
}
$("#customSwitch2").change(function () {
    var isChecked = document.getElementById("customSwitch2").checked;
    var url = new URL(window.location.href);
    var search_params = url.searchParams;

    if (isChecked) {
        search_params.set('haveDiscount', "true");
        url.search = search_params.toString();
        var new_url = url.toString();

        window.location.replace(new_url);
    } else {
        search_params.delete("haveDiscount");
        url.search = search_params.toString();
        var new_url = url.toString();
        window.location.replace(new_url);
    }
});
$("#customSwitch1").change(function () {
    var isChecked = document.getElementById("customSwitch1").checked;
    var url = new URL(window.location.href);
    var search_params = url.searchParams;

    if (isChecked) {
        search_params.set('justAvailableProducts', "true");
        url.search = search_params.toString();
        var new_url = url.toString();

        window.location.replace(new_url);
    } else {
        var url = new URL(window.location.href);
        search_params.set('justAvailableProducts', "false");
        url.search = search_params.toString();
        var new_url = url.toString();
        window.location.replace(new_url);
    }
});
