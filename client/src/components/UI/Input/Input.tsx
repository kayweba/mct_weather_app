import cls from './Input.module.css'

type Props = Omit<
  React.DetailedHTMLProps<
    React.InputHTMLAttributes<HTMLInputElement>,
    HTMLInputElement
  >,
  'className'
>;

export function Input(props: Props) {
  return <input className={cls.defaultInputStyle} {...props} />;
}
