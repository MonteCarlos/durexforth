code (do)
pla, zptmp sta,
pla, tay,

sp1 1+ lda,x pha, sp0 1+ lda,x pha,
sp1 lda,x pha, sp0 lda,x pha,
inx, inx, 

tya, pha,
zptmp lda, pha,
;code

: do ( limit first -- ) immediate
postpone (do) here ;

code (loop)
zptmp stx, tsx, \ x = stack pointer
103 inc,x 3 bne, 104 inc,x \ i++
103 lda,x 105 cmp,x 1 @@ beq, \ lsb
2 @:
\ not done, branch back
zptmp ldx, \ restore x
loc branch dup assert >cfa jmp,
1 @:
104 lda,x 106 cmp,x 2 @@ bne, \ msb
\ loop done
\ skip branch addr
pla, clc, 3 adc,# zptmp2 sta,
pla, 0 adc,# zptmp2 1+ sta,
txa, clc, 6 adc,# tax, txs, \ sp += 6
zptmp ldx, \ restore x
zptmp2 (jmp),

: loop immediate no-tce
postpone (loop) , ; \ store branch address

: (+loop) ( inc -- )
r> swap r> tuck + tuck 
2dup > if swap then
r@ 1- -rot within 0= if 
>r >r [ ' branch jmp, ] then
r> 2drop 2+ >r ;

: +loop immediate no-tce
postpone (+loop) , ;
hide (+loop)

: i immediate postpone r@ ;
code j txa, tsx,
107 ldy,x zptmp sty, 108 ldy,x
tax, dex, 
sp1 sty,x zptmp lda, sp0 sta,x ;code
