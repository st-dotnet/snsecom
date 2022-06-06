var productIndex = function () {
    var editProduct = function (ctrl) {
        var id = $(ctrl).attr("data-id");
        $.ajax({
            type: "GET",
            url: "/Product/GetProduct/" + id,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                $("#productHdnId").val(data.productId);
                $("#productSKU").val(data.sku);
                $("#productPrice").val(data.price);
                $("#productQuantity").val(data.quantity);
                $("#productDescription").val(data.description);
                $("#productDetailModal").show();
            }
        });
    };

    var updateProduct = function () {
        debugger;
        var postData = {
            ProductId: $("#productHdnId").val(),
            SKU: $("#productSKU").val(),
            Price: $("#productPrice").val(),
            Quantity: $("#productQuantity").val(),
            Description: $("#productDescription").val()
        }
             $.ajax({
                 type: "POST",
                 url: "/Product/UpdateProduct",
                 data: postData,
                 success: function (data) {
                     if (data) {
                         debugger;
                         $("#productDetailModal").hide();
                     }
                     else {
                         alert("Not Updated");
                     }
                 }
             });
        }

    return {
        //main function to initiate the module
        init: function () {

            //Open product detail in modal
            $(document).on("click", "#productDetail", function () {
                editProduct($(this));
            });

            //update product detail
            $(document).on("click", "#updateProduct", function () {
                updateProduct($(this));
            });

            //Close upload csv file modal
            $(document).on("click", ".closeModal", function () {
                $("#uploadFileModal").hide();
            });

            //Open upload csv file popup
            $(document).on("click", "a#btnUplaodFile", function () {
                $("#uploadFileModal").show();
            });

            $(document).on("click", ".closeEditModal", function () {
                $("#productDetailModal").hide();
            });
        }
    };
}();