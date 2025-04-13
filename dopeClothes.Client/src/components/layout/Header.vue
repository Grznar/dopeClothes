<template>
  <!--Navbar-->
  <nav
    class="navbar navbar-expand-lg navbar-light sticky-top scrolled-navbar border-bottom"
  >
    <div class="container-fluid">
      <a href="" class="navbar-brand ms-7">
        <img
          width="50"
          height="50"
          src="/src/assets/images/navLogo.png"
          alt=""
          class="align-text-top"
        />
      </a>
      <button
        class="navbar-toggler"
        data-bs-toggle="collapse"
        data-bs-target="#navbar"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse justify-content-between" id="navbar">
        <!-- Levé menu -->
        <ul class="navbar-nav ms-5">
          <li class="nav-item">
            <a href="#" class="nav-link">Home</a>
          </li>
          <li class="nav-item"><a href="#" class="nav-link">Products</a></li>
          <li class="nav-item"><a href="#" class="nav-link">Contact</a></li>
        </ul>

        <!-- Pravé menu -->
        <ul class="navbar-nav me-7">
          <li class="nav-item"><a href="#" class="nav-link">Login</a></li>
          <li class="nav-item"><a href="#" class="nav-link">Register</a></li>
        </ul>
      </div>
    </div>
  </nav>
</template>
