window.initializeInfiniteScroll = function (dotnetHelper) {
    function handleScroll() {
        if ((window.innerHeight + window.scrollY) >= document.documentElement.scrollHeight - 100) {
            dotnetHelper.invokeMethodAsync('OnScroll');
        }
    }

    window.addEventListener('scroll', handleScroll);
};