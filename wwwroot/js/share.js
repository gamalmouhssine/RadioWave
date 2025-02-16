function shareApp() {
    if (navigator.share) {
        navigator.share({
            title: "RadioWave - Listen to the Best Radio Stations",
            text: "Check out RadioWave! Discover and listen to your favorite radio stations worldwide.",
            url: "https://play.google.com/store/apps/details?id=com.yourapp.radiowave"
        }).then(() => {
            console.log("App shared successfully!");
        }).catch((err) => {
            console.error("Error sharing:", err);
        });
    } else {
        alert("Sharing is not supported on this browser.");
    }
}
