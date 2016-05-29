These codepage files have been extracted from the Pawn binaries which can be
downloaded from http://www.compuphase.com/pawn/pawn.htm

Lines should comply with the following format:
codepage value
|     TAB
|     | unicode value
|     | |     TAB
|     | |     | (optional) comment
v     v v     v v
0x0000  0x0000  # COMMENT

Any line not starting with '0x' will be skipped.