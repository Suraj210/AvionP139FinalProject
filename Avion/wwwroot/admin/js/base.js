$(function () {

    $(document).on("click", ".image-delete button", function (e) {

        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            url: `/admin/product/deleteproductimage?id=${id}`,
            type: "Post",
            success: function (res) {
                $(e.target).parent().remove();
            }
        })

    })
})