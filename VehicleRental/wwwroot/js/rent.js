document.addEventListener("DOMContentLoaded", function () {
    const rentModal = document.getElementById("rentModal");
    const rentalDaysInput = document.getElementById("rentalDays");
    let vehiclePrice = 0;

    if (rentModal) {
        rentModal.addEventListener("show.bs.modal", function (event) {
            const button = event.relatedTarget;
            const vehicleId = button.getAttribute("data-vehicle-id");
            const vehicleName = button.getAttribute("data-vehicle-name");
            vehiclePrice = parseFloat(button.getAttribute("data-vehicle-price")) || 0;

            document.getElementById("vehicleId").value = vehicleId;
            document.getElementById("vehicleName").textContent = vehicleName;
            document.getElementById("vehiclePrice").textContent = `$${vehiclePrice.toFixed(2)}/day`;

            updateTotalCost();

            // Prevent duplicate event listeners
            rentalDaysInput.removeEventListener("input", updateTotalCost);
            rentalDaysInput.addEventListener("input", updateTotalCost);
        });
    }

    function updateTotalCost() {
        const days = Math.max(1, parseInt(rentalDaysInput.value) || 1);
        const total = (vehiclePrice * days).toFixed(2);
        document.getElementById("totalCost").textContent = `$${total}`;
    }
});
