function shadowNavbar() {
  const navbar = document.querySelector(".navbar");
  if (window.scrollY > 10) {
    navbar.classList.add("scrolled-navbar");
    navbar.classList.add("border-bottom");
  } else {
    if (navbar.classList.contains("scrolled-navbar")) {
      navbar.classList.remove("scrolled-navbar");
      navbar.classList.remove("border-bottom");
    }
  }
}
window.addEventListener("scroll", shadowNavbar);
