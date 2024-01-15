$(document).ready(function () {
  $("div[data-slick]").slick();
  $(".product-container").slick({
    dots: true,
    infinite: true,
    speed: 300,
    slidesToShow: 4,
    slidesToScroll: 1,
    responsive: [
      {
        breakpoint: 992,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          infinite: true,
          dots: true,
        },
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,
          dots: true,
        },
      },
      {
        breakpoint: 576,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,
          dots: true,
        },
      },
    ],
  });
});

// Card Js

let buy = document.querySelectorAll("#products .buy");
let remove = document.querySelectorAll("#products .remove");

buy.forEach((elem) => {
  elem.addEventListener("click", function () {
    this.parentNode.parentNode.classList.add("clicked");
  });
});

remove.forEach((elem) => {
  elem.addEventListener("click", function () {
    this.parentNode.parentNode.classList.remove("clicked");
  });
});

// Card wishlist Js

let wishlist = document.querySelectorAll("#products .wishlist");

wishlist.forEach((elem) => {
  elem.addEventListener("click", function () {
    this.classList.toggle("added");
  });
});
