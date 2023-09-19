import { ChangeEvent, useState } from "react";
import { SignInDto } from "../../models/Account";
import { Link } from "react-router-dom";

const SignIn = () => {
  const [signIn, setSignIn] = useState<SignInDto>({
    email: "",
    password: "",
  });

  const handelChanged = (event: ChangeEvent<HTMLInputElement>): void => {
    setSignIn(() => {
      return {
        ...signIn,
        [event.target.name]: event.target.value,
      };
    });
  };

  const handelSubmit = async (
    event: ChangeEvent<HTMLFormElement>
  ): Promise<void> => {
    event.preventDefault();
  };

  const content: JSX.Element = (
    <>
      <div className="h-[80px]"></div>
      <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900 dark:dark-mode-text-primary">
            Sign in to your account
          </h2>
        </div>

        <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          <form className="space-y-6" onSubmit={handelSubmit}>
            <div className="form-group">
              <label htmlFor="email">Email address</label>
              <div className="mt-2">
                <input
                  className="form-input"
                  id="email"
                  name="email"
                  type="email"
                  autoComplete="email"
                  required
                  onChange={handelChanged}
                  value={signIn.email}
                />
              </div>
            </div>

            <div className="form-group">
              <div className="flex items-center justify-between form-group">
                <label htmlFor="password">Password</label>
                <div className="text-sm">
                  <a href="#" className="hyperlink">
                    Forgot password?
                  </a>
                </div>
              </div>
              <div className="mt-2">
                <input
                  className="form-input"
                  id="password"
                  name="password"
                  type="password"
                  autoComplete="current-password"
                  required
                  onChange={handelChanged}
                  value={signIn.password}
                />
              </div>
            </div>

            <div>
              <button
                className="flex w-full justify-center rounded-md px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2"
                type="submit">
                Sign in
              </button>
            </div>
          </form>

          <p className="mt-10 text-center text-sm dark:dark-mode-text-secondary">
            Not a member?{" "}
            <Link to={"/Account/SignUp"} className="hyperlink">
              Sign up
            </Link>
          </p>
        </div>
      </div>
    </>
  );
  return content;
};

export default SignIn;
