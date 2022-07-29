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

$(document).ready(function () {
    $.ajax({
        url: "/shopcart/shopcartDetail",
        type: "get"
    }).done(function (data) {
        var bagElements = $(".bag-items-number");
        try {
            bagElements[0].innerHTML = data.count;
            bagElements[1].innerHTML = data.count;
        } catch {

        }
        $(".cart-footer .total").html(data.price)
        if (data.items.length == 0) {
            $('.cart-list ul').remove();
        } else {
            data.items.map((i) => {
                $(".cart-items .do-nice-scroll").append(`
                        <li class="cart-item">
                                    <span class="d-flex align-items-center mb-2">
                                        <a href="/product/${i.productSlug}">
                                            <img src="https://localhost:5001/images/products/${i.productImageName}" alt="">
                                        </a>
                                        <span>
                                            <a href="#">
                                                <span class="title-item">
                                                    ${i.productTitle}
                                                </span>
                                            </a>
                                            <span class="color d-flex align-items-center">
                                                تعداد:
                                                <label style='display:contents'>${i.count}</label>
                                            </span>
                                        </span>
                                    </span>
                                    <span class="price">${splitNumber(i.totalPrice)} تومان</span>
                                    <button class="remove-item" onclick="DeleteItem('/ShopCart/DeleteItem?id=${i.id}','')">
                                        <i class="far fa-trash-alt"></i>
                                    </button>
                                </li>
            `);
            })
        }
        
    });
});

function splitNumber(value) {
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}