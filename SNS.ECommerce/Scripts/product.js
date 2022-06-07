var productIndex = function ()
{

    var addProduct = function ()
    {
        debugger;
        var postData = {
            SKU: $("#addproductSKU").val(),
            Price: $("#addproductPrice").val(),
            Quantity: $("#addproductQuantity").val(),
            Description: $("#addproductDescription").val()
        }
        $.ajax({
            type: "POST",
            url: "/Product/Create",
            data: postData,
            success: function (data) {
                if (data) {
                    debugger;
                    $("#productAddModal").hide();
                    location.reload();
                }
                else {
                    alert("Not Updated");
                }
            }
        });
    }
    var detailProduct = function (ctrl) {
        debugger;
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


    var delProduct = function (ctrl) {
        debugger;
        var id = $(ctrl).attr("data-id");
        $.ajax({
            type: "GET",
            url: "/Product/Delete/" + id,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data)
            {
                location.reload();
            }
        });
    };

    var editProduct = function (ctrl) {
        debugger;
        var id = $(ctrl).attr("data-id");
        $.ajax({
            type: "GET",
            url: "/Product/GetProduct/" + id,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data)
            {
                $("#editproductHdnId").val(data.productId);
                $("#editproductSKU").val(data.sku);
                $("#editproductPrice").val(data.price);
                $("#editproductQuantity").val(data.quantity);
                $("#editproductDescription").val(data.description);
                $("#productEditModal").show();
            }
        });
    };


    var updateProduct = function () {
        debugger;
        var postData = {
            ProductId: $("#editproductHdnId").val(),
            SKU: $("#editproductSKU").val(),
            Price: $("#editproductPrice").val(),
            Quantity: $("#editproductQuantity").val(),
            Description: $("#editproductDescription").val()
        }
             $.ajax({
                 type: "POST",
                 url: "/Product/UpdateProduct",
                 data: postData,
                 success: function (data) {
                     if (data) {
                         debugger;
                         $("#productEditModal").hide();
                         location.reload();
                     }
                     else {
                         alert("Not Updated");
                     }
                 }
             });
    }

    return {
        //main function to initiate the module
        init: function ()
        {

            //Open product detail in modal
            $(document).on("click", "#productDetail", function () {
                detailProduct($(this));
            });

            //update product detail
            $(document).on("click", "#updateProduct", function () {
                updateProduct($(this));
            });
              //delete product detail
            $(document).on("click", "#productDelete", function ()
            {
                var checkstr = confirm('are you sure you want to delete this?');
                if (checkstr == true)
                {
                    delProduct($(this));
                } else {return false;}
               
            });
            //delete product detail
            $(document).on("click", "#productEdit", function () {
                    editProduct($(this));
              
            });

            //Open Model for add product
            $(document).on("click", "#btnproductAdd", function () {
                $("#productAddModal").show();
              //  addProduct($(this));

            });
            //submit product
            $(document).on("click", "#productAdd", function () {
                addProduct($(this));
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

            //Close productEditModal
            $(document).on("click", ".editProductModalClose", function () {
                $("#productEditModal").hide();
            });

            //Close productAddModal
            $(document).on("click", ".addProductModalClose", function () {
                $("#productAddModal").hide();
            });
        }
    };
}();