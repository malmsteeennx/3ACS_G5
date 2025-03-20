document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        document.getElementById("loader").style.display = "none";
        document.body.style.overflow = "auto"; // Enable scrolling after loader disappears
    }, 1000); // 1-second delay to ensure smooth transition
});
