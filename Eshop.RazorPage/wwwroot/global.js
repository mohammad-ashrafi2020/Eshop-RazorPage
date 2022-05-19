function Success(title, description, isReload = false) {
    if (title == null || title == "undefined") {
        title = "عملیات با موفقیت انجام شد";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: title,
        text: description,
        icon: "success",
        confirmButtonText: "باشه",
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}
function Info(Title, description) {
    if (Title == null || Title == "undefined") {
        Title = "توجه";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "info",
        confirmButtonText: "باشه"
    });
}
function ErrorAlert(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "مشکلی در عملیات رخ داده است";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    console.log(description);

    Swal.fire({
        title: Title,
        html: description,
        icon: "error",
        confirmButtonText: "باشه"
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}
function Warning(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "مشکلی در عملیات رخ داده است";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "warning",
        confirmButtonText: "باشه"
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}

function DeleteItem(url, description) {
    Swal.fire({
        title: "آیا از حذف اطمینان دارید ؟",
        text: description,
        icon: "warning",
        confirmButtonText: "بله ، مطمعا هستم",
        cancelButtonText: "خیر",
        showCancelButton: true,
    }).then((result) => {
        if (result.isConfirmed) {
            var token = $("#ajax-token input[name='__RequestVerificationToken']").val();
            $.ajax({
                url: url,
                method: "post",
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
                if (res.Status === 1) {
                    Success("", res.Message, true);
                } else {
                    ErrorAlert("", res.Message, res.isReloadPage);
                }
            });
        }
    });
}
function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return decodeURIComponent(c.substring(name.length, c.length));
        }
    }
    return "";
}

function deleteCookie(cookieName) {
    document.cookie = `${cookieName}=;expires=Thu, 01 Jan 1970;path=/`;
}
$(document).ready(function () {
    var result = getCookie("SystemAlert");
    if (result) {
        result = JSON.parse(result);
        if (result.IsSuccess === true) {
            Success("", result.MetaData.Message, result.isReloadPage);
        } else {
            ErrorAlert("", result.MetaData.Message, result.isReloadPage);
        }
        deleteCookie("SystemAlert");
    }
    if ($(".select2")) {
        $(".select2").select2();
    }
});

function OpenModal(url, name, title) {
    var modalSize = 'modal-lg';
    $(`#${name} .modal-body`).html('');

    $.ajax({
        url: url,
        type: "get",
        beforeSend: function () {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
    }).done(function (result) {
        result = JSON.parse(result);

        if (result.Status != 1) {
            ErrorAlert("مشکلی رخ داده", result.Message);
            return;
        }

        if (result.Data) {

            $('#' + name + ' .modal-body').html(result.Data);
            $('#' + name + ' .modal-title ').html(title);

            $('#' + name).modal({
                backdrop: 'static',
                keyboard: true
            },
                'show');

            $('#' + name + ' .modal-dialog').removeClass('modal-lg modal-xl modal-sm modal-full');
            $('#' + name + ' .modal-dialog').addClass(modalSize);

            const form = $("#" + name + ' form');
            if (form) {
                $.validator.unobtrusive.parse(form);
            }
        }
    });

}

function CallBackHandler(result) {
    if (result.Status == 1) {
        Success(result.Title, result.Message, result.IsReloadPage);
    } else {
        ErrorAlert(result.Title, result.Message, result.IsReloadPage);
    }

}
$(document).on("submit",
    'form[data-ajax="true"]',
    function (e) {
        e.preventDefault();
        var form = $(this);
        const method = form.attr("method").toLocaleLowerCase();
        const url = form.attr("action");
        if (method === "get") {
            const data = form.serializeArray();
            $.get(url,
                data,
                function (data) {
                    CallBackHandler(data);
                });
        } else {
            var formData = new FormData(this);
            $.ajax({
                url: url,
                type: "post",
                data: formData,
                enctype: "multipart/form-data",
                dataType: "json",
                processData: false,
                contentType: false,
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
                success: function (data) {
                    CallBackHandler(data);
                },
                error: function (data) {
                    ErrorAlert();
                }
            });
        }
        return false;
    });


