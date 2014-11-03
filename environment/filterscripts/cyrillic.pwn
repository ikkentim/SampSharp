#include <a_samp>

public OnFilterScriptInit()
{
SendClientMessageToAll(0xFFFF22AA, " Привет");
SendClientMessageToAll(0xFFFF22AA, " Тестирование кирилических символов");
SendClientMessageToAll(0xFFFF22AA, " Алфавит");
SendClientMessageToAll(0xFFFF22AA, " АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ");
SendClientMessageToAll(0xFFFF22AA, " абвгдеёжзийклмнопрстуфхцчшщьыъэюя");
return 1;
}