import { HttpUtils } from '../utils/http-utils.js';

const baseApiUrl = '/api/gmail-widget/'

document.addEventListener('DOMContentLoaded', () => {
    // event listeners
    document.getElementById('authneticate-btn')
        ?.addEventListener('click', () => {
            authneticateUser();
        });

    // web api calls
    function authneticateUser() {
        fetch(baseApiUrl + 'authenticate')
            .then((response) => {
                if (!response.ok) {
                    HttpUtils.throwError(response);
                }
                else {
                    location.reload();
                }
            })
            .catch((error) => {
                HttpUtils.handleError(error);
            })
    };
});
