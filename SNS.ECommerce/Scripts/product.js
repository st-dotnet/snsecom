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

    var deleteProduct = function () {
        var id = $("#productHdnDeleteId").val();
        $.ajax({
            type: "DELETE",
            url: "/Product/DeleteProduct/" + id,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data) {
                    alert("Product Deleted Sucecssfully");
                    $("#productDetailModal").hide();
                }
                else {
                    alert("Error");
                }
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
                         $("#productDeleteModal").hide();
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

            //Delete Product
            $(document).on("click", "#deleteProductId", function () {
                deleteProduct($(this));
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

            //Close the Edit modal
            $(document).on("click", ".closeEditModal", function () {
                $("#productDetailModal").hide();
            });

            //close the Delete Modal by 'x' icon
            $(document).on("click", "#closeModal", function () {
                $("#productDeleteModal").hide();
            });

            //close the Delete Modal By Button
            $(document).on("click", "#closeDeleteModalBtn", function () {
                $("#productDeleteModal").hide();
            });

            //Delete product modal show
            $(document).on("click", "#deleteProduct", function () {
                $("#productHdnDeleteId").val($(this).attr("data-productid"));
                $("#productDeleteModal").show();
            });
        }
    };
}();