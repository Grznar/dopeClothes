<template>
  <section class="dashboard-section">
    <div class="container-fluid">
      <div class="row">
        <!-- Levý sloupec: Karty -->
        <div class="col-12 col-md-6 left-column">
          <div class="cards-wrapper">
            <div class="row justify-content-center mb-5">
              <div
                class="col-lg-4 col-md-6 mb-4"
                v-for="card in statsCards"
                :key="card.title"
              >
                <div class="card stat-card shadow-sm">
                  <div class="card-body text-center">
                    <h5 class="card-title text-uppercase">{{ card.title }}</h5>
                    <p class="card-text display-4">{{ formatValue(card) }}+</p>
                    <p class="card-text">{{ card.description }}</p>
                  </div>
                </div>
              </div>
            </div>

            <div class="row justify-content-center">
              <div class="col-lg-8">
                <div class="card contact-card shadow-sm">
                  <div class="card-body text-center">
                    <h5 class="card-title text-uppercase">Contact</h5>
                    <p class="card-text">Email: jakubcerny2005@seznam.cz</p>
                    <p class="card-text">Phone: xxx xxx xxx xxx</p>
                    <p class="card-text">Address: CZ</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- Pravý sloupec: Obrázek -->
        <div class="col-12 col-md-6 right-column d-none d-md-block p-0">
          <img
            src="@/assets/images/contact.png"
            alt="Large display"
            class="right-image"
          />
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { reactive, onMounted } from "vue";

const statsCards = reactive([
  {
    title: "Served Clients",
    value: 1200,
    description: "Happy customers",
    currentValue: 0,
  },
  {
    title: "Products",
    value: 350,
    description: "Quality items",
    currentValue: 0,
  },
  {
    title: "Rating",
    value: 4.8,
    description: "Out of 5 stars",
    currentValue: 0,
  },
]);

function animateValue(card, duration = 2000) {
  const startTime = performance.now();
  const endValue = card.value;

  function updateValue(timestamp) {
    const elapsed = timestamp - startTime;
    const progress = Math.min(elapsed / duration, 1);
    card.currentValue = progress * endValue;
    if (progress < 1) {
      requestAnimationFrame(updateValue);
    } else {
      card.currentValue = endValue;
    }
  }
  requestAnimationFrame(updateValue);
}

function formatValue(card) {
  return Number.isInteger(card.value)
    ? Math.floor(card.currentValue)
    : card.currentValue.toFixed(1);
}

onMounted(() => {
  statsCards.forEach((card) => {
    animateValue(card, 2000);
  });
});
</script>

<style scoped>
.dashboard-section {
  min-height: 100vh;
  background-color: #f8f9fa; /* světlé pozadí celé sekce */
  color: #333;
}

.container-fluid {
  padding: 0;
}

.row {
  margin: 0;
}

/* Levý sloupec */
.left-column {
  background-color: #f8f9fa;
  padding: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.cards-wrapper {
  width: 100%;
}

/* Styl karty s efekty */
.card {
  background-color: #333;
  border: none;
  border-radius: 12px;
  overflow: hidden;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

/* Zvýraznění efektu při hoveru */
.card:hover {
  transform: scale(1.05);
  box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.3);
}

.card-body {
  padding: 2rem;
}

.card-title {
  font-weight: bold;
  color: #fff;
  margin-bottom: 1rem;
}

.display-4 {
  font-size: 2.5rem;
  font-weight: 700;
  color: #fff;
  margin-bottom: 0.5rem;
}

.card-text {
  color: #ddd;
  font-size: 1.1rem;
  margin-bottom: 0.5rem;
}

/* Pravý sloupec: obrázek */
.right-column {
  overflow: hidden;
}

.right-image {
  width: 100%;
  height: 100vh;
  object-fit: cover;
}

@media (max-width: 767.98px) {
  .right-column {
    display: none;
  }
}
</style>
