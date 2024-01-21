
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
$(document).on("submit", ".product-searchbox", function (e) {
    e.preventDefault();
    let value = $(".input-search").val();
    let url = `/Shop/Search?searchText=${value}`;
    window.location.assign(url);

})
})