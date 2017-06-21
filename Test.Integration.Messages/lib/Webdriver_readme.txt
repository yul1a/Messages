После апдейта WebDriver.dll ее нужно пропатчить чтобы кнопка остановки тестов в NUnit не подвисала.

Для этого 
1) Берем исходники: git clone https://code.google.com/p/selenium/
2) Идем в ChromeDriverService и при создани процесса ставим ему UseShellExecute = true.