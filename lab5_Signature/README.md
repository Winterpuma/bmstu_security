# Цифоровая подпись

### Задание
Реализовать подписывание документа и проверку подписи.   
Использовать готовые библиотеки для шифрования и хеширования разрешено и даже приветствуется.

### Вопросы
* Какие два важных шага при создании подписи?   
  Хеширование подписываемого файла и шифрование его закрытым ключом.
* Как происходит проверка?  
  Вычисляется хеш файла с данными, расшифровывается файл с хешем открым ключом, полученные два хеша должны совпасть.
* Чем отличается подпись от сертификата

### Подпись vs Сертификат
Для проверки сообщения используется цифровая подпись . Это в основном зашифрованное hash (зашифрованное закрытым ключом отправителя) сообщение. Получатель может проверить, не было ли сообщение подделано, хэшируя полученное сообщение и сравнивая это значение с расшифрованной подписью.

Для расшифровки подписи требуется соответствующий открытый ключ. Цифровой сертификат используется для привязки открытых ключей к физическим или иным лицам. Если бы не было сертификатов, подпись можно было бы легко подделать, так как получатель не мог бы проверить, принадлежит ли открытый ключ отправителю.

Сам сертификат подписывается доверенной третьей стороной, центром сертификации , например VeriSign.
