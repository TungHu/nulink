var __funcaptchaInitParameters={responses:{lastSolution:null}};(function(){var t={};if(document.currentScript&&document.currentScript.dataset&&document.currentScript.dataset["parameters"]){try{t=JSON.parse(document.currentScript.dataset["parameters"])}catch(o){}}if(t.originalFuncaptchaApiUrl){var n=document.createElement("a");n.href=t.originalFuncaptchaApiUrl;if(n.hostname){__funcaptchaInitParameters.apiJSSubdomain=n.hostname}}if(t.originalFuncaptchaApiUrl&&t.currentFuncaptchaApiUrl&&t.originalFuncaptchaApiUrl!==t.currentFuncaptchaApiUrl){var a=document.getElementsByTagName("script");for(var e in a){if(a[e].src===t.originalFuncaptchaApiUrl){a[e].src=t.currentFuncaptchaApiUrl;break}}}else{}var r=t.currentOnloadMethodName;if(r){function c(){var t;if(typeof window[r]==="function"){t=window[r]}window[r]=function(){var n=FunCaptcha;FunCaptcha=ArkoseEnforcement=function(t){if(t&&typeof t.callback=="function"){var a=t.callback;t.callback=function(){a.apply(this,arguments)}}Object.assign(__funcaptchaInitParameters,t);n.apply(this,arguments);this.getSessionToken=function(){if(__funcaptchaInitParameters["responses"]["lastSolution"]){return __funcaptchaInitParameters["responses"]["lastSolution"]}}};ArkoseEnforcement.prototype.clear_session=function(){n.prototype.clear_session.apply(this,arguments)};ArkoseEnforcement.prototype.refresh_session=function(){n.prototype.refresh_session.apply(this,arguments)};if(typeof t==="function"){t.apply(this,arguments)}}}if(typeof window[r]!=="undefined"||r==="_funcaptchaOnloadMethod"){c()}else{var i=setInterval((function(){if(typeof window[r]==="undefined"){return}clearInterval(i);c()}),1)}}})();