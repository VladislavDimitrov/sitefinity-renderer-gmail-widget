import { HttpUtils } from '../utils/http-utils.js';

const baseApiUrl = '/api/gmail-widget/'

document.addEventListener('DOMContentLoaded', () => {
    const widgetData = {
        recipients: $("#recipients"),
        subject: document.getElementById('email-subject'),
        emailBody: $("#email-body"),
        recipientsValidator: null,
    };

    //const recipients = $("#recipients");
    //const subject = document.getElementById('email-subject');
    //const emailBody = $("#email-body");

    // event listeners
    document.getElementById('sign-out-btn')
        ?.addEventListener('click', () => {
            signOutUser();
        });

    document.getElementById('send-gmail-btn')
        ?.addEventListener('click', () => {
            sendGmail();
        });

    // initialize Kendo widgets
    widgetData.emailBody.kendoEditor();

    getContactsPromise()
        .then((contactsResponse) => {
            let modifiedContacts = contactsResponse.map(c => ({
                recipientName: `${c.recipientName}(${c.recipientEmaillAddress})`,
                recipientInfo: { name: c.recipientName, emailAddress: c.recipientEmaillAddress }
            }));

            let recipientsDataSource = new kendo.data.DataSource({
                data: modifiedContacts
            });

            widgetData.recipients.kendoMultiSelect({
                dataTextField: "recipientName",
                dataValueField: "recipientInfo",
                dataSource: recipientsDataSource,
                filter: "contains",
                placeholder: "Enter recipients",
                downArrow: true,
            });
        });


    // web api calls
    function getContactsPromise() {
        let promise = fetch(baseApiUrl + 'get-contacts')
            .then((response) => {
                if (!response.ok) {
                    HttpUtils.throwError(response);
                }
                else {
                    return response.json();
                }
            })
            .catch((error) => {
                HttpUtils.handleError(error);
            });

        return promise
    }

    function signOutUser() {
        fetch(baseApiUrl + 'sign-out')
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
    }

    function sendGmail() {
        if (!sendGmailRequestIsValid()) {
            return;
        }
        
        const payload = {
            recipients: widgetData.recipients.data("kendoMultiSelect").value(),
            emailSubject: widgetData.subject.value,
            emailBody: widgetData.emailBody.data("kendoEditor").value()
        };
        
        const fetchOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json', 
            },
            body: JSON.stringify(payload),
        }

        fetch(baseApiUrl + 'send-gmail', fetchOptions)
            .then((response) => {
                if (!response.ok) {
                    HttpUtils.throwError(response);
                }
                else {
                    if (window.confirm(`You have successfully sent this email. Would you like to remain logged in to the Gmail Widget to send another email? \r\n
                    If you select 'Cancel' you will be automatically signed out of the widget.`)) {
                        return;
                    } else {
                        signOutUser();
                    }
                }
            })
            .catch((error) => {
                HttpUtils.handleError(error);
            })
    }

    // utility functions
    function sendGmailRequestIsValid() {
        const recipientsValidator = getRecipientsKendoValidator();
        if (!recipientsValidator.validate()) {
            return false;
        }
        else {
            //TODO: make sure the errors are hidden if there were any shown on the screen
            recipientsValidator.hideMessages();
        }

        let validationMessage = 'Are you sure you want to send this email without';
        const validationConditions = [];
        if (!widgetData.subject.value) {
            validationConditions.push('Subject');
        }
        if (!widgetData.emailBody.data("kendoEditor").value()) {
            validationConditions.push('Email body');
        }

        if (validationConditions.length > 0) {
            validationMessage += ' ' + validationConditions.join(' and ');
            return window.confirm(validationMessage);
        }

        return true;
    }

    function getRecipientsKendoValidator() {
        if (!widgetData.recipientsValidator) {
            widgetData.recipientsValidator = widgetData.recipients.kendoValidator({
                rules: {
                    hasItems: function (input) {
                        var ms = input.data("kendoMultiSelect");
                        if (ms.value().length < 1) {
                            return false;
                        }
                        
                        return true;
                    }
                },
                messages: {
                    hasItems: "Please select at least 1 recipient."
                }
            }).data("kendoValidator");
        }

        return widgetData.recipientsValidator;
    }
});
