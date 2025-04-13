const navbar = document.querySelector(".navbar");

function shadowNavbar() {
  if (window.scrollY > 10) {
    navbar.classList.add("scrolled-navbar");
    navbar.classList.add("border-bottom");
  } else {
    navbar.classList.remove("scrolled-navbar");
    navbar.classList.remove("border-bottom");
  }
}
window.addEventListener("scroll", shadowNavbar);
