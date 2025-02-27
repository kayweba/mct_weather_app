import { ReactNode } from 'react';
import cls from './Button.module.css';


type Props = {
  onClick: (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void
  children: ReactNode
}

export function Button({ onClick, children }: Props) {
  return (
    <button className={cls.btn} onClick={(event) => onClick(event)}>
      {children}
    </button>
  )
}
