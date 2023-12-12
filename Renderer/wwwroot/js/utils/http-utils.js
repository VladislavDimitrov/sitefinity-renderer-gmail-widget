export const HttpUtils = {

    // functions for error handling that are used accross the application and can be extended in a single place
    throwError: function (response) {
        throw new Error(`HTTP error! Status: ${response.status}`);
    },

    handleError: function (error) {
        console.error(error);
        window.alert(error);
    }
}