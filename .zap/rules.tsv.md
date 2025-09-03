# ZAP Scanning Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 1 |
| Medium | 2 |
| Low | 5 |
| Informational | 9 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Vulnerable JS Library | High | 1 |
| Content Security Policy (CSP) Header Not Set | Medium | 2 |
| Missing Anti-clickjacking Header | Medium | 2 |
| Insufficient Site Isolation Against Spectre Vulnerability | Low | 11 |
| Permissions Policy Header Not Set | Low | 4 |
| Strict-Transport-Security Header Not Set | Low | 9 |
| Timestamp Disclosure - Unix | Low | 34 |
| X-Content-Type-Options Header Missing | Low | 7 |
| Base64 Disclosure | Informational | 3 |
| Information Disclosure - Suspicious Comments | Informational | 2 |
| Modern Web Application | Informational | 2 |
| Re-examine Cache-control Directives | Informational | 2 |
| Sec-Fetch-Dest Header is Missing | Informational | 6 |
| Sec-Fetch-Mode Header is Missing | Informational | 6 |
| Sec-Fetch-Site Header is Missing | Informational | 6 |
| Sec-Fetch-User Header is Missing | Informational | 6 |
| Storable and Cacheable Content | Informational | 10 |




## Alert Detail



### [ Vulnerable JS Library ](https://www.zaproxy.org/docs/alerts/10003/)



##### High (Medium)

### Description

The identified library appears to be vulnerable.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `var a="dompurify"+(n?"#"+n:"");try{return t.createPolicy(a,{createHTML:function(e){return e},createScriptURL:function(e){return e}})}catch(e){return console.warn("TrustedTypes policy "+a+" could not be created."),null}};function ne(){var t=arguments.length>0&&void 0!==arguments[0]?arguments[0]:te(),r=function(e){return ne(e)};if(r.version="2.3.10"`
  * Other Info: `The identified library DOMPurify, version 2.3.10 is vulnerable.
CVE-2024-47875
CVE-2025-26791
CVE-2024-48910
CVE-2024-45801
https://github.com/advisories/GHSA-gx9m-whjm-85jf
https://github.com/cure53/DOMPurify/releases/tag/3.2.4
https://github.com/cure53/DOMPurify/commit/d18ffcb554e0001748865da03ac75dd7829f0f02
https://github.com/cure53/DOMPurify/commit/6ea80cd8b47640c20f2f230c7920b1f4ce4fdf7a
https://github.com/advisories/GHSA-p3vf-v8qc-cwcr
https://github.com/cure53/DOMPurify/commit/0ef5e537a514f904b6aa1d7ad9e749e365d7185f
https://github.com/cure53/DOMPurify/security/advisories/GHSA-p3vf-v8qc-cwcr
https://nvd.nist.gov/vuln/detail/CVE-2024-45801
https://github.com/advisories/GHSA-vhxf-7vqr-mrjg
https://github.com/cure53/DOMPurify/security/advisories/GHSA-mmhx-hmjr-r674
https://github.com/cure53/DOMPurify/commit/d1dd0374caef2b4c56c3bd09fe1988c3479166dc
https://github.com/cure53/DOMPurify
https://github.com/advisories/GHSA-mmhx-hmjr-r674
https://github.com/cure53/DOMPurify/commit/26e1d69ca7f769f5c558619d644d90dd8bf26ebc
https://nvd.nist.gov/vuln/detail/CVE-2025-26791
https://nvd.nist.gov/vuln/detail/CVE-2024-47875
https://github.com/cure53/DOMPurify/security/advisories/GHSA-gx9m-whjm-85jf
https://github.com/cure53/DOMPurify/blob/0ef5e537a514f904b6aa1d7ad9e749e365d7185f/test/test-suite.js#L2098
https://ensy.zip/posts/dompurify-323-bypass
https://nsysean.github.io/posts/dompurify-323-bypass
https://github.com/cure53/DOMPurify/commit/1e520262bf4c66b5efda49e2316d6d1246ca7b21
https://nvd.nist.gov/vuln/detail/CVE-2024-48910
`

Instances: 1

### Solution

Upgrade to the latest version of the affected library.

### Reference


* [ https://owasp.org/Top10/A06_2021-Vulnerable_and_Outdated_Components/ ](https://owasp.org/Top10/A06_2021-Vulnerable_and_Outdated_Components/)


#### CWE Id: [ 1395 ](https://cwe.mitre.org/data/definitions/1395.html)


#### Source ID: 3

### [ Content Security Policy (CSP) Header Not Set ](https://www.zaproxy.org/docs/alerts/10038/)



##### Medium (High)

### Description

Content Security Policy (CSP) is an added layer of security that helps to detect and mitigate certain types of attacks, including Cross Site Scripting (XSS) and data injection attacks. These attacks are used for everything from data theft to site defacement or distribution of malware. CSP provides a set of standard HTTP headers that allow website owners to declare approved sources of content that browsers should be allowed to load on that page — covered types are JavaScript, CSS, HTML frames, fonts, images and embeddable objects such as Java applets, ActiveX, audio and video files.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 2

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Content-Security-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy ](https://developer.mozilla.org/en-US/docs/Web/Security/CSP/Introducing_Content_Security_Policy)
* [ https://cheatsheetseries.owasp.org/cheatsheets/Content_Security_Policy_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/Content_Security_Policy_Cheat_Sheet.html)
* [ https://www.w3.org/TR/CSP/ ](https://www.w3.org/TR/CSP/)
* [ https://w3c.github.io/webappsec-csp/ ](https://w3c.github.io/webappsec-csp/)
* [ https://web.dev/articles/csp ](https://web.dev/articles/csp)
* [ https://caniuse.com/#feat=contentsecuritypolicy ](https://caniuse.com/#feat=contentsecuritypolicy)
* [ https://content-security-policy.com/ ](https://content-security-policy.com/)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Missing Anti-clickjacking Header ](https://www.zaproxy.org/docs/alerts/10020/)



##### Medium (Medium)

### Description

The response does not protect against 'ClickJacking' attacks. It should include either Content-Security-Policy with 'frame-ancestors' directive or X-Frame-Options.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `x-frame-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: `x-frame-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 2

### Solution

Modern Web browsers support the Content-Security-Policy and X-Frame-Options HTTP headers. Ensure one of them is set on all web pages returned by your site/app.
If you expect the page to be framed only by pages on your server (e.g. it's part of a FRAMESET) then you'll want to use SAMEORIGIN, otherwise if you never expect the page to be framed, you should use DENY. Alternatively consider implementing Content Security Policy's "frame-ancestors" directive.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options)


#### CWE Id: [ 1021 ](https://cwe.mitre.org/data/definitions/1021.html)


#### WASC Id: 15

#### Source ID: 3

### [ Insufficient Site Isolation Against Spectre Vulnerability ](https://www.zaproxy.org/docs/alerts/90004/)



##### Low (Medium)

### Description

Cross-Origin-Resource-Policy header is an opt-in header designed to counter side-channels attacks like Spectre. Resource should be specifically set as shareable amongst different origins.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui.css
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Cross-Origin-Embedder-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: `Cross-Origin-Embedder-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Cross-Origin-Opener-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: `Cross-Origin-Opener-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 11

### Solution

Ensure that the application/web server sets the Cross-Origin-Resource-Policy header appropriately, and that it sets the Cross-Origin-Resource-Policy header to 'same-origin' for all web pages.
'same-site' is considered as less secured and should be avoided.
If resources must be shared, set the header to 'cross-origin'.
If possible, ensure that the end user uses a standards-compliant and modern web browser that supports the Cross-Origin-Resource-Policy header (https://caniuse.com/mdn-http_headers_cross-origin-resource-policy).

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Cross-Origin_Resource_Policy ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Cross-Origin_Resource_Policy)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 14

#### Source ID: 3

### [ Permissions Policy Header Not Set ](https://www.zaproxy.org/docs/alerts/10063/)



##### Low (Medium)

### Description

Permissions Policy Header is an added layer of security that helps to restrict from unauthorized access or usage of browser/client features by web resources. This policy ensures the user privacy by limiting or specifying the features of the browsers can be used by the web resources. Permissions Policy provides a set of standard HTTP headers that allow website owners to limit which features of browsers can be used by the page such as camera, microphone, location, full screen etc.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 4

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Permissions-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy)
* [ https://developer.chrome.com/blog/feature-policy/ ](https://developer.chrome.com/blog/feature-policy/)
* [ https://scotthelme.co.uk/a-new-security-header-feature-policy/ ](https://scotthelme.co.uk/a-new-security-header-feature-policy/)
* [ https://w3c.github.io/webappsec-feature-policy/ ](https://w3c.github.io/webappsec-feature-policy/)
* [ https://www.smashingmagazine.com/2018/12/feature-policy/ ](https://www.smashingmagazine.com/2018/12/feature-policy/)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Strict-Transport-Security Header Not Set ](https://www.zaproxy.org/docs/alerts/10035/)



##### Low (High)

### Description

HTTP Strict Transport Security (HSTS) is a web security policy mechanism whereby a web server declares that complying user agents (such as a web browser) are to interact with it using only secure HTTPS connections (i.e. HTTP layered over TLS/SSL). HSTS is an IETF standards track protocol and is specified in RFC 6797.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui.css
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 9

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to enforce Strict-Transport-Security.

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/HTTP_Strict_Transport_Security_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/HTTP_Strict_Transport_Security_Cheat_Sheet.html)
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)
* [ https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security ](https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security)
* [ https://caniuse.com/stricttransportsecurity ](https://caniuse.com/stricttransportsecurity)
* [ https://datatracker.ietf.org/doc/html/rfc6797 ](https://datatracker.ietf.org/doc/html/rfc6797)


#### CWE Id: [ 319 ](https://cwe.mitre.org/data/definitions/319.html)


#### WASC Id: 15

#### Source ID: 3

### [ Timestamp Disclosure - Unix ](https://www.zaproxy.org/docs/alerts/10096/)



##### Low (Low)

### Description

A timestamp was disclosed by the application/web server. - Unix

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1467031594`
  * Other Info: `1467031594, which evaluates to: 2016-06-27 12:46:34.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1495990901`
  * Other Info: `1495990901, which evaluates to: 2017-05-28 17:01:41.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1501505948`
  * Other Info: `1501505948, which evaluates to: 2017-07-31 12:59:08.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1508970993`
  * Other Info: `1508970993, which evaluates to: 2017-10-25 22:36:33.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1518500249`
  * Other Info: `1518500249, which evaluates to: 2018-02-13 05:37:29.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1522805485`
  * Other Info: `1522805485, which evaluates to: 2018-04-04 01:31:25.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1537002063`
  * Other Info: `1537002063, which evaluates to: 2018-09-15 09:01:03.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1541459225`
  * Other Info: `1541459225, which evaluates to: 2018-11-05 23:07:05.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1546045734`
  * Other Info: `1546045734, which evaluates to: 2018-12-29 01:08:54.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1555081692`
  * Other Info: `1555081692, which evaluates to: 2019-04-12 15:08:12.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1575990012`
  * Other Info: `1575990012, which evaluates to: 2019-12-10 15:00:12.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1595750129`
  * Other Info: `1595750129, which evaluates to: 2020-07-26 07:55:29.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1607167915`
  * Other Info: `1607167915, which evaluates to: 2020-12-05 11:31:55.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1654270250`
  * Other Info: `1654270250, which evaluates to: 2022-06-03 15:30:50.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1694076839`
  * Other Info: `1694076839, which evaluates to: 2023-09-07 08:53:59.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1695183700`
  * Other Info: `1695183700, which evaluates to: 2023-09-20 04:21:40.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1731405415`
  * Other Info: `1731405415, which evaluates to: 2024-11-12 09:56:55.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1732584193`
  * Other Info: `1732584193, which evaluates to: 2024-11-26 01:23:13.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1747873779`
  * Other Info: `1747873779, which evaluates to: 2025-05-22 00:29:39.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1750603025`
  * Other Info: `1750603025, which evaluates to: 2025-06-22 14:37:05.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1779033703`
  * Other Info: `1779033703, which evaluates to: 2026-05-17 16:01:43.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1816402316`
  * Other Info: `1816402316, which evaluates to: 2027-07-24 04:11:56.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1856431235`
  * Other Info: `1856431235, which evaluates to: 2028-10-29 11:20:35.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1859775393`
  * Other Info: `1859775393, which evaluates to: 2028-12-07 04:16:33.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1894007588`
  * Other Info: `1894007588, which evaluates to: 2030-01-07 09:13:08.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1899447441`
  * Other Info: `1899447441, which evaluates to: 2030-03-11 08:17:21.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1914138554`
  * Other Info: `1914138554, which evaluates to: 2030-08-28 09:09:14.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1925078388`
  * Other Info: `1925078388, which evaluates to: 2031-01-01 23:59:48.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1955562222`
  * Other Info: `1955562222, which evaluates to: 2031-12-20 19:43:42.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1986661051`
  * Other Info: `1986661051, which evaluates to: 2032-12-14 18:17:31.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1996064986`
  * Other Info: `1996064986, which evaluates to: 2033-04-02 14:29:46.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `2003034995`
  * Other Info: `2003034995, which evaluates to: 2033-06-22 06:36:35.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `2007800933`
  * Other Info: `2007800933, which evaluates to: 2033-08-16 10:28:53.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `2024104815`
  * Other Info: `2024104815, which evaluates to: 2034-02-21 03:20:15.`

Instances: 34

### Solution

Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.

### Reference


* [ https://cwe.mitre.org/data/definitions/200.html ](https://cwe.mitre.org/data/definitions/200.html)


#### CWE Id: [ 497 ](https://cwe.mitre.org/data/definitions/497.html)


#### WASC Id: 13

#### Source ID: 3

### [ X-Content-Type-Options Header Missing ](https://www.zaproxy.org/docs/alerts/10021/)



##### Low (Medium)

### Description

The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui.css
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`

Instances: 7

### Solution

Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.
If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.

### Reference


* [ https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85) ](https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85))
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Base64 Disclosure ](https://www.zaproxy.org/docs/alerts/10094/)



##### Informational (Medium)

### Description

Base64 encoded data was disclosed by the application/web server. Note: in the interests of performance not all base64 strings in the response were analyzed individually, the entire response should be looked at by the analyst/security team/developer(s).

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `authorizeOauth2WithPersistOption`
  * Other Info: `j�a���x殶���Oz�"�ө�*'`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/`
  * Other Info: ` �Q� ��0ӏA�QU�a��qן���Y�����ۯ���]�㞻�߿`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui.css
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `iVBORw0KGgoAAAANSUhEUgAAAAgAAAAICAYAAADED76LAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyhpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTExIDc5LjE1ODMyNSwgMjAxNS8wOS8xMC0wMToxMDoyMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MTRDOTY4N0U2N0VFMTFFNjg2MzZDQjkwNkQ4MjgwMEIiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6MTRDOTY4N0Q2N0VFMTFFNjg2MzZDQjkwNkQ4MjgwMEIiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUgKE1hY2ludG9zaCkiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDo3NjcyQkQ3NjY3QzUxMUU2QjJCQ0UyNDA4MTAwMjE3MSIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDo3NjcyQkQ3NzY3QzUxMUU2QjJCQ0UyNDA4MTAwMjE3MSIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PsBS+GMAAAAjSURBVHjaYvz//z8DLsD4gcGXiYEAGBIKGBne//fFpwAgwAB98AaF2pjlUQAAAABJRU5ErkJggg==`
  * Other Info: `�PNG

   IHDR         ���   tEXtSoftware Adobe ImageReadyq�e<  (iTXtXML:com.adobe.xmp     <?xpacket begin="﻿" id="W5M0MpCehiHzreSzNTczkc9d"?> <x:xmpmeta xmlns:x="adobe:ns:meta/" x:xmptk="Adobe XMP Core 5.6-c111 79.158325, 2015/09/10-01:10:20        "> <rdf:RDF xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#"> <rdf:Description rdf:about="" xmlns:xmpMM="http://ns.adobe.com/xap/1.0/mm/" xmlns:stRef="http://ns.adobe.com/xap/1.0/sType/ResourceRef#" xmlns:xmp="http://ns.adobe.com/xap/1.0/" xmpMM:DocumentID="xmp.did:14C9687E67EE11E68636CB906D82800B" xmpMM:InstanceID="xmp.iid:14C9687D67EE11E68636CB906D82800B" xmp:CreatorTool="Adobe Photoshop CC 2015 (Macintosh)"> <xmpMM:DerivedFrom stRef:instanceID="xmp.iid:7672BD7667C511E6B2BCE24081002171" stRef:documentID="xmp.did:7672BD7767C511E6B2BCE24081002171"/> </rdf:Description> </rdf:RDF> </x:xmpmeta> <?xpacket end="r"?>�R�c   #IDATx�b���?.������� 
���ŧ  � }��ژ�Q    IEND�B`�`

Instances: 3

### Solution

Manually confirm that the Base64 data does not leak sensitive information, and that the data cannot be aggregated/used to exploit other vulnerabilities.

### Reference


* [ https://projects.webappsec.org/w/page/13246936/Information%20Leakage ](https://projects.webappsec.org/w/page/13246936/Information%20Leakage)


#### CWE Id: [ 319 ](https://cwe.mitre.org/data/definitions/319.html)


#### WASC Id: 13

#### Source ID: 3

### [ Information Disclosure - Suspicious Comments ](https://www.zaproxy.org/docs/alerts/10027/)



##### Informational (Low)

### Description

The response appears to contain suspicious comments which may help an attacker.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `xxx`
  * Other Info: `The following pattern was used: \bXXX\b and was detected in likely comment: "//,""):-1!==l()(e).call(e,"#/components/schemas/")?e.replace(/^.*#\/components\/schemas\//,""):void 0)),i()(this,"getRefSchema",", see evidence field for the suspicious comment/snippet.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `user`
  * Other Info: `The following pattern was used: \bUSER\b and was detected in likely comment: "//reactjs.org/docs/error-decoder.html?invariant="+t,r=1;r<arguments.length;r++)e+="&args[]="+encodeURIComponent(arguments[r]);re", see evidence field for the suspicious comment/snippet.`

Instances: 2

### Solution

Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.

### Reference



#### CWE Id: [ 615 ](https://cwe.mitre.org/data/definitions/615.html)


#### WASC Id: 13

#### Source ID: 3

### [ Modern Web Application ](https://www.zaproxy.org/docs/alerts/10109/)



##### Informational (Medium)

### Description

The application appears to be a modern web application. If you need to explore it automatically then the Ajax Spider may well be more effective than the standard one.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script>(function (document) {

  // Set the document title.
  var elements = document.getElementsByClassName('info_title');
  if (elements.length === 1) {
    document.title = elements[0].innerHTML;
  }

  // Set the document favicon.
  var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
  link.type = 'image/x-icon';
  link.rel = 'shortcut icon';
  link.href = '/swagger/favicon.ico';
  document.getElementsByTagName('head')[0].appendChild(link);

}(window.document));
</script>`
  * Other Info: `No links have been found while there are scripts, which is an indication that this is a modern web application.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script>(function (document) {

  // Set the document title.
  var elements = document.getElementsByClassName('info_title');
  if (elements.length === 1) {
    document.title = elements[0].innerHTML;
  }

  // Set the document favicon.
  var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
  link.type = 'image/x-icon';
  link.rel = 'shortcut icon';
  link.href = '/swagger/favicon.ico';
  document.getElementsByTagName('head')[0].appendChild(link);

}(window.document));
</script>`
  * Other Info: `No links have been found while there are scripts, which is an indication that this is a modern web application.`

Instances: 2

### Solution

This is an informational alert and so no changes are required.

### Reference




#### Source ID: 3

### [ Re-examine Cache-control Directives ](https://www.zaproxy.org/docs/alerts/10015/)



##### Informational (Low)

### Description

The cache-control header has not been set properly or is missing, allowing the browser and proxies to cache content. For static assets like css, js, or image files this might be intended, however, the resources should be reviewed to ensure that no sensitive content will be cached.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 2

### Solution

For secure content, ensure the cache-control HTTP header is set with "no-cache, no-store, must-revalidate". If an asset should be cached consider setting the directives "public, max-age, immutable".

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching ](https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching)
* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control)
* [ https://grayduck.mn/2021/09/13/cache-control-recommendations/ ](https://grayduck.mn/2021/09/13/cache-control-recommendations/)


#### CWE Id: [ 525 ](https://cwe.mitre.org/data/definitions/525.html)


#### WASC Id: 13

#### Source ID: 3

### [ Sec-Fetch-Dest Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies how and where the data would be used. For instance, if the value is audio, then the requested resource must be audio data and not any other type of resource.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/robots.txt
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 6

### Solution

Ensure that Sec-Fetch-Dest header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Dest ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Dest)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-Mode Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Allows to differentiate between requests for navigating between HTML pages and requests for loading resources like images, audio etc.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/robots.txt
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 6

### Solution

Ensure that Sec-Fetch-Mode header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Mode ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Mode)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-Site Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies the relationship between request initiator's origin and target's origin.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/robots.txt
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 6

### Solution

Ensure that Sec-Fetch-Site header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Site ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Site)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-User Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies if a navigation request was initiated by a user.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/robots.txt
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/sitemap.xml
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 6

### Solution

Ensure that Sec-Fetch-User header is included in user initiated requests.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Storable and Cacheable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are storable by caching components such as proxy servers, and may be retrieved directly from the cache, rather than from the origin server by the caching servers, in response to similar requests from other users. If the response data is sensitive, personal or user-specific, this may result in sensitive information being leaked. In some cases, this may even result in a user gaining complete control of the session of another user, depending on the configuration of the caching components in use in their environment. This is primarily an issue where "shared" caching servers such as "proxy" caches are configured on the local network. This configuration is typically found in corporate or educational environments, for instance.

* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-16x16.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/favicon-32x32.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/index.html
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-bundle.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui-standalone-preset.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com/swagger/swagger-ui.css
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`

Instances: 10

### Solution

Validate that the response does not contain sensitive, personal or user-specific information. If it does, consider the use of the following HTTP response headers, to limit, or prevent the content being stored and retrieved from the cache by another user:
Cache-Control: no-cache, no-store, must-revalidate, private
Pragma: no-cache
Expires: 0
This configuration directs both HTTP 1.0 and HTTP 1.1 compliant caching servers to not store the response, and to not retrieve the response (without validation) from the cache, in response to a similar request.

### Reference


* [ https://datatracker.ietf.org/doc/html/rfc7234 ](https://datatracker.ietf.org/doc/html/rfc7234)
* [ https://datatracker.ietf.org/doc/html/rfc7231 ](https://datatracker.ietf.org/doc/html/rfc7231)
* [ https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html ](https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html)


#### CWE Id: [ 524 ](https://cwe.mitre.org/data/definitions/524.html)


#### WASC Id: 13

#### Source ID: 3


