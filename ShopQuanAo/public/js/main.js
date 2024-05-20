
function addcart(productID) {
    var quantity = $("#sel1").val();
    $.ajax({
        url: '/them-sp-giohang?productID=' + productID + '&quantity=' + quantity + '',
        type: 'GET',
        beforeSend: function () {

            $('.loading-ajax-effect').fadeIn('slow');
            $('#alert-class').removeClass("success");
            $('#alert-class').removeClass("danger");
            $('#alert-class').removeClass("warning");
            $('.loading-ajax-effect').fadeOut('slow');
        },
        success: function (data) {
            if (data.meThod == "updateQuantity") {
                if (data.bad == true) {
                    alert("Bạn đã chọn số lượng lớn hơn số lượng hiện có trong kho:");
                    return false;
                }
                else {
                    if (data.priceSale > 0) {
                        var sum = data.ProductPrice * data.quantity;
                    }
                    else {
                    var sum = data.price * data.quantity;
                    }
                    $('#totalPrice').text(data.priceTotol + " VND");
                    $("#quantity_" + data.productID + "").text(data.quantity);
                    $("#sum_" + data.productID + "").text(sum + " VND");
                }
            }
            //
            else if (data.meThod == "cartExist") {
                if (data.f == true) {
                    alert("Bạn đã chọn số lượng lớn hơn số lượng hiện có trong kho:");
                    return false;
                } else { 
                $('#count-cart-header').html(data.countCart);
                $('#count-cart-nav').html(data.countCart);
                $('#totalPrice').text(data.priceTotal);
                if (data.product.pricesale > 0) {
                    var sum = data.priceSaleee * data.quantity;
                }
                else {
                    var sum = data.product.price * data.quantity;
                }
                if (data.product.pricesale > 0) {
                    var productItem = "<li class='row cart-detail border-bottom position-relative'><a style='top:37%; left:-10px;' class='position-absolute text-danger' href='#'><i class='far fa-trash-alt'></i></a><div class='col-sm cart-detail-img'> <img src='/public/images/" + data.product.img + "' ></div > " + " <div class='col-sm-8  cart-detail-product' > " + "<a href = '#' > <p>" + data.product.name + "</p></a > <span><strike>" + data.product.price + "VND </br></strike> </span> <span class='price text-cam' > " + data.priceSaleee + " VND</span > x <span id='quantity_" + data.product.ID + "' class='price text-info'> " + data.quantity + " </span> = <span class='text-info' id='sum_" + data.product.ID + "'> " + sum + " VND</span> </div ></li>"
                }
                else {
                    var productItem = "<li class='row cart-detail border-bottom position-relative'><a style='top:37%; left:-10px;' class='position-absolute text-danger' href='#'><i class='far fa-trash-alt'></i></a><div class='col-sm cart-detail-img'> <img src='/public/images/" + data.product.img + "' ></div > " + " <div class='col-sm-8  cart-detail-product' > " + "<a href = '#' > <p>" + data.product.name + "</p></a >  <span  id='price_" + data.product.ID + "' class='price text-cam' > " + data.product.price + " VND</span > x <span id='quantity_" + data.product.ID + "' class='price text-info'> " + data.quantity + " </span> = <span class='text-info' id='sum_" + data.product.ID + "'> " + sum + " VND</span> </div ></li>"
                }
                    $(".overflow-auto1").append(productItem);
                }
            }
                //done
            else if (data.meThod == "cartEmpty") {
                if (data.f == true) {
                    alert("Bạn đã chọn số lượng lớn hơn số lượng hiện có trong kho:");
                    return false;
                }
                else {
                $('#count-cart-header').html(data.countCart);
                $('#count-cart-nav').html(data.countCart);
                var sum = data.priceTotal;
                $('#totalPrice').text(sum + " VND");
                if (data.product.pricesale > 0) {
                    var productItem = "<li class='row cart-detail border-bottom position-relative'><a style='top:37%; left:-10px;' class='position-absolute text-danger' href='#'><i class='far fa-trash-alt'></i></a><div class='col-sm cart-detail-img'> <img src='/public/images/" + data.product.img + "' ></div > " + " <div class='col-sm-8  cart-detail-product' > " + "<a href = '#' > <p>" + data.product.name + "</p></a > <span><strike>" + data.product.price + "VND </br></strike> </span> <span class='price text-cam' > " + sum + " VND</span > x <span id='quantity_" + data.product.ID + "' class='price text-info'> " + data.quantity + " = </span> <span class='text-info' id='sum_" + data.product.ID + "'> " + sum + " VND</span></div ></li>"
                }
                else {
                    var productItem = "<li class='row cart-detail border-bottom position-relative'><a style='top:37%; left:-10px;' class='position-absolute text-danger' href='#'><i class='far fa-trash-alt'></i></a><div class='col-sm cart-detail-img'> <img src='/public/images/" + data.product.img + "' ></div > " + " <div class='col-sm-8  cart-detail-product' > " + "<a href = '#' > <p>" + data.product.name + "</p></a >  <span  class='price text-cam' > " + data.product.price + " VND</span > x <span id='quantity_" + data.product.ID + "' class='price text-info'> " + data.quantity + " = </span> <span class='text-info' id='sum_" + data.product.ID + "'> " + sum + " VND</span></div ></li>"
                }
                    $(".overflow-auto1").append(productItem);
                }
            }
            setTimeout(function () {
                $('#alert-class').addClass("success");
                $('#alert-mess').text('Thêm sản phẩm vào giỏ hàng thành công');
                $('#custom-alert-ajax').fadeIn('slow');
            }, 700);
            setTimeout(function () {
                $(".alertContainer").fadeOut("slow");
            }, 5000);

        }
    })
}


function addfavorite(productID) {
    $.ajax({
        url: '/addFavorite?productID=' + productID + '',
        type: 'GET',
        beforeSend: function () {
            $('#alert-class').removeClass("warning");
            $('#alert-class').removeClass("success");
            $('.ajaxload-effect-heart').fadeIn('slow');
            $('.ajaxload-effect-heart').fadeOut('slow');
        },
        success: function (data) {
            if (data.status == 1) {
                setTimeout(function () {
                    $('#alert-class').addClass("warning");
                    $('#alert-mess').text('Sản phẩm đã có trong Danh sách yêu thích rồi.');
                    $('#custom-alert-ajax').fadeIn('slow');
                }, 700);
            }
            else if (data.status == 3) {
                setTimeout(function () {
                    $('#alert-class').addClass("success");
                    $('#alert-mess').text('Đã thêm Vào Danh sách Yêu Thích Thành Công');
                    $('#custom-alert-ajax').fadeIn('slow');
                }, 700);
            }
            setTimeout(function () {
                $(".alertContainer").fadeOut("slow");
            }, 5000);

        }
    })
}

