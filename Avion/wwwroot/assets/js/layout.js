
let sideBar = document.querySelector("aside .sidebar");
let sideBarToggle = document.querySelectorAll(".sideBarToggle");
let overlay = document.querySelector(".overlay");

sideBarToggle.forEach((elem) => {
  elem.addEventListener("click", function () {
    sideBar.classList.toggle("transformLeft");
    overlay.classList.toggle("d-none");
  });
});

overlay.addEventListener("click", function () {
  overlay.classList.toggle("d-none");
  sideBar.classList.toggle("transformLeft");
});



$(function () {

    //Search JS
    $(document).on("submit", ".product-searchbox", function (e) {
        e.preventDefault();
        let value = $(".input-search").val();
        let url = `/Shop/Search?searchText=${value}`;
        window.location.assign(url);

    });

    //Sort JS

    $(document).on("change", "#sortProducts", function (e) {
        e.preventDefault();
        let sortValue = $(this).val();
        let url = `/Shop/Sort?sortValue=${sortValue}`;
        window.location.assign(url);
    });


    //Basket JS
    $(document).on("click", ".cart-add", function () {
        let id = $(this).attr("data-id");;
        let count = $(".basket-count").text();
        $.ajax({
            url: `/shop/addbasket?id=${id}`,
            type: "Post",
            success: function (res) {

                count++;
                $(".basket-count").text(count);

            }
        })

    })

    $(document).on("click", ".basket-table .basket-plus", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket-count").text();
        $.ajax({

            url: `basket/plusicon?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).prev().children().attr("value",res.countItem)
                $(".grandTotal").text("$" + res.grandTotal.toFixed(2));
                $(e.target).parent().next().children().text("$" + res.productTotalPrice.toFixed(2) )
                count++;

                $(".basket-count").text(count);
            }
        })

    })

    $(document).on("click", ".basket-table .basket-minus", function (e) {

        let id = parseInt($(this).attr("data-id"))
        let count = $(".basket-count").text();
        let a = 0;

        $.ajax({

            url: `basket/minusicon?id=${id}`,
            type: "Post",
            success: function (res) {

                $(e.target).next().children().attr("value", res.countItem)
                $(".grandTotal").text("$" +res.grandTotal.toFixed(2));
                $(e.target).parent().next().children().text("$" + res.productTotalPrice.toFixed(2))
                $(".basket-count").text(res.countBasket)

            }
        })

    })



    $(document).on("click", ".delete-basket-item", function (e) {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `basket/delete?id=${id}`,
            type: "Post",
            success: function (res) {


                $(".basket-count").text(res.count);
                $(e.target).closest(".addedProduct").remove();
                $(".grandTotal").text("$" + res.grandTotal);

                if (res.count === 0) {
                    $(".empty").removeClass("d-none");
                    $(".basket-table").addClass("d-none");
                }


            }
        })


    })

})