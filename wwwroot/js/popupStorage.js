document.addEventListener("DOMContentLoaded", function () {
    checkAndShowPopup();
});

function checkAndShowPopup() {
    const lastPopupTime = localStorage.getItem("lastPopupTime");
    const currentTime = new Date().getTime();
    const thirtyDays = 30 * 24 * 60 * 60 * 1000; // 30 days in milliseconds

    if (!lastPopupTime || (currentTime - lastPopupTime > thirtyDays)) {
        showRatePopup();
        localStorage.setItem("lastPopupTime", currentTime);
    }
}

function showRatePopup() {
    const popup = document.createElement("div");
    popup.className = "rate-popup";
    popup.innerHTML = `
        <div class="popup-content">
            <h3>Enjoying RadioWave?</h3>
            <p>If you love our app, please give us a 5-star rating!</p>
            <button onclick="rateApp()">⭐ Rate Now</button>
            <button onclick="closePopup()">Maybe Later</button>
        </div>
    `;
    document.body.appendChild(popup);
}

function closePopup() {
    document.querySelector(".rate-popup").remove();
}

function rateApp() {
    window.open("https://play.google.com/store/apps/details?id=com.yourapp.radiowave", "_blank");
    closePopup();
}
