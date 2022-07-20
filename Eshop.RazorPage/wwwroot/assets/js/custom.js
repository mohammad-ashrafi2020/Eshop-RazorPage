function activeAddress(addressId) {
    Swal.fire({
        title: "آیا از انجام عملیات اطمینان دارید ؟",
        icon: "info",
        confirmButtonText: "بله ، مطمعا هستم",
        cancelButtonText: "خیر",
        showCancelButton: true,
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/profile/addresses/SetActiveAddress?addressId=" + addressId,
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
            }).done(function (data) {
                var res = JSON.parse(data);
                if (res.Status === 1) {
                    Success("", res.Message, true);
                } else {
                    ErrorAlert("", res.Message, res.isReloadPage);
                }
            })
        }
    });
}
function addToCart(inventoryId, count) {
    var token = $("#ajax-token input[name='__RequestVerificationToken']").val();

    $.ajax({
        url: `/shopcart/addItem?inventoryId=${inventoryId}&count=${count}`,
        type: "post",
        data: {
            __RequestVerificationToken: token
        },
        beforeSend: function (xhr) {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
    }).done(function (data) {
        var res = JSON.parse(data);
        CallBackHandler(res);
    });
}