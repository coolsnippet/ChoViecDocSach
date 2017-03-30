simple definitions:

base64: used to represent binary data in text or ASCII chars format
64 = 2^6 (6 bits of data per character) A-Z, a-z, 0-9, + and -
so we take a group of 6 bytes from the binary data
so 3 bytes (24 bits) can be represented with 4 base64 digits
and we use "=" to denote the missing 
