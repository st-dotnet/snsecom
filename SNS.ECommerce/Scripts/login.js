var loginIndex = function () {
    var submitLogin = function (ctrl) {

    };
    return {
        //main function to initiate the module
        init: function () {
            $(document).on("click", "#loginForm", function () {
                submitLogin($(this));
            });
        }
    };
}();