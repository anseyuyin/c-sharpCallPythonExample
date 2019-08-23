
def commandParse(str):
    if str == 'a':
        print('输入a')
    elif str == 'b':
        print('输出b')
    else:
        print('指令错误 : ',str)

while True:
    command = input()
    commandParse(command)
