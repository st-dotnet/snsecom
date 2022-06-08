var loginIndex = function () {
    var submitLogin = function () {
        debugger;
        $.ajax({
            type: "POST",
            url: "/Account/LogIn",
            data: { "email": $("#email").val(), "password": $("#password").val() },
            success: function (response) {
                window.location.href = '/Product/Index/';
            },
            failure: function (response) {
                window.location.href = '/LogIn/Index/';
            },
            error: function (response) {
                window.location.href = '/LogIn/Index/';
            }
        });
    };
    return {
        //main function to initiate the module
        init: function () {
            $(document).on("click", "#loginForm", function () {
                submitLogin();
            });
        }
    };
}();