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