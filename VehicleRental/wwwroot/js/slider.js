document.addEventListener('DOMContentLoaded', function () {
    const slides = document.querySelectorAll('.about-us-slide');
    let currentSlide = 0;

    function showSlide(index) {
        slides.forEach((slide, i) => {
            slide.classList.remove('active', 'prev', 'next');

            if (i === index) {
                slide.classList.add('active');
            } else if (i < index) {
                slide.classList.add('prev'); // Moves left
            } else {
                slide.classList.add('next'); // Moves right
            }
        });
    }

    function nextSlide() {
        currentSlide = (currentSlide < slides.length - 1) ? currentSlide + 1 : 0;
        showSlide(currentSlide);
    }

    // Auto-slide every 5 seconds
    setInterval(nextSlide, 5000);

    showSlide(currentSlide);
});
