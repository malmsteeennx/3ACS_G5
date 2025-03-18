document.addEventListener('DOMContentLoaded', function () {
    const slides = document.querySelectorAll('.about-us-slide');
    let currentSlide = 0;

    function showSlide(index) {
        slides.forEach((slide, i) => {
            slide.classList.remove('active');
            if (i === index) {
                slide.classList.add('active');
            }
        });
    }

    document.querySelectorAll('.prev-slide').forEach(button => {
        button.addEventListener('click', function () {
            currentSlide = (currentSlide > 0) ? currentSlide - 1 : slides.length - 1;
            showSlide(currentSlide);
        });
    });

    document.querySelectorAll('.next-slide').forEach(button => {
        button.addEventListener('click', function () {
            currentSlide = (currentSlide < slides.length - 1) ? currentSlide + 1 : 0;
            showSlide(currentSlide);
        });
    });

    showSlide(currentSlide);
});