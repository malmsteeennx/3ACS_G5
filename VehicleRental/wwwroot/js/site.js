// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//added password toggle
    function togglePassword(inputId) {
                            const input = document.getElementById(inputId);
    const button = input.nextElementSibling.querySelector('i');

    if (input.type === 'password') {
        input.type = 'text';
    button.classList.replace('bi-eye', 'bi-eye-slash');
                            } else {
        input.type = 'password';
    button.classList.replace('bi-eye-slash', 'bi-eye');
                            }
                                                }